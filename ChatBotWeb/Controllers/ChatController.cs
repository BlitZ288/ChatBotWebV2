using ChatBotWeb.Service;
using ChatBotWeb.Service.BotService.Interface;
using ChatBotWeb.Service.ChatServices.Interface;
using ChatBotWeb.Service.UserService.Interface;
using ChatBotWeb.ViewModel;
using Coman;
using Domian.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatBotWeb.Controllers
{
    public class ChatController : Controller
    {
        private IChatService chatService;
        private IMessageService messageService;
        private IUserService userService;
        private IBotService botService;

        public ChatController(IChatService chatService, IMessageService messageService, IUserService userService, IBotService botService)
        {
            this.chatService = chatService;
            this.messageService = messageService;
            this.userService = userService;
            this.botService = botService;
        }
        [Authorize]
        public IActionResult Index(int idChat)
        {
            var chat = chatService.GetChatById(idChat);
            var currentUser = userService.GetUserByName(User.FindFirst(ClaimTypes.Name).Value);

            ViewData["NameUser"] = currentUser.Name;

            var logUser = chat.ChatLogUsers.FirstOrDefault(l => l.User.Id == currentUser.Id);

            if (logUser == null)
            {
                chat.Users.Add(currentUser);
                chat.ChatLogUsers.Add(new LogsUser() { StartChat = DateTime.Now, StopChat = null, User = currentUser });

                chat.LogActions.Add(new LogAction(DateTime.Now, EventChat.JoinChat, currentUser));

                chatService.UpdateChat(chat);
            }
            else if (!chat.Users.Contains(currentUser))
            {
                chat.Users.Add(currentUser);
                chat.ChatLogUsers.FirstOrDefault(l => l.LogsUserId == logUser.LogsUserId).StopChat = null;
                chatService.UpdateChat(chat);
            }

            var availableUsers = userService.GetAllUser().Except(chat.Users);

            var chatBots = userService.GetBotsByIdChat(idChat);
            IEnumerable<User> availableBots;

            if (chatBots.Any())
            {
                availableBots = userService.GetAllBots().Except(chatBots);
            }
            else
            {
                availableBots = userService.GetAllBots();
            }

            ChatUserViewModel model = new ChatUserViewModel()
            {
                ChatId = idChat,
                UsersNotInclude = availableUsers,
                ChatUsers = chat.Users,
                HistoryChat = messageService.HistoryMessagesForChat(idChat, currentUser.Id),
                CurrentUser = currentUser,
                AvailableBots = availableBots,
                ChatBots = chatBots
            };

            if (TempData["refresh"] != null)
            {
                HttpContext.Response.Headers.Add("refresh", "15; url=" + Url.Action("Index", new
                {
                    idChat = idChat
                }));
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult AddMessageInChat(string content, int chatId)
        {
            if (!String.IsNullOrEmpty(content))
            {
                var currentUser = userService.GetUserByName(User.FindFirst(ClaimTypes.Name).Value);
                var chat = chatService.GetChatById(chatId);

                Message message = new Message(content, currentUser);
                chat.ListMessage.Add(message);

                chatService.UpdateChat(chat);

                Task.Run(() => botService.ProcessTextMessage(content, chatId));

                TempData["refresh"] = true;
            }
            else
            {
                TempData["Errors"] = "Нельзя отправить пустое сообщение";
            }
            return RedirectToAction("Index", new { idChat = chatId });
        }
        [HttpPost]
        public ActionResult DeleteMessage(int messageId, int chatId)
        {
            var message = messageService.GetMessageById(messageId);
            var currentUser = userService.GetUserByName(User.FindFirst(ClaimTypes.Name).Value);

            if (message.User.Id == currentUser.Id)
            {
                messageService.DeleteMessage(messageId);

                return RedirectToAction("Index", new { idChat = chatId });
            }
            else
            {
                TempData["Errors"] = "Вы не можете удалить сообщение не принадлежащее вам";
                return RedirectToAction("Index", new { idChat = chatId });
            }
        }
        [HttpPost]
        public ActionResult AddUserInChat(int userId, int chatId)
        {
            var user = userService.GetUser(userId);
            var currentUser = userService.GetUserByName(User.FindFirst(ClaimTypes.Name).Value);

            var chat = chatService.GetChatById(chatId);

            var logUser = chat.ChatLogUsers.FirstOrDefault(l => l.User.Id == userId);
            if (logUser == null)
            {
                LogsUser logs = new LogsUser() { StartChat = DateTime.Now, StopChat = null, User = user };
                chat.ChatLogUsers.Add(logs);
            }
            else
            {
                logUser.StopChat = null;
            }
            chat.Users.Add(user);

            LogAction logAction = new LogAction(DateTime.Now, EventChat.JoinChat, currentUser);
            chat.LogActions.Add(logAction);

            chatService.UpdateChat(chat);

            Task.Run(() => botService.ProcessEvenInChat(logAction.Content, chatId));

            return RedirectToAction("Index", new { idChat = chatId });
        }

        [HttpPost]
        public ActionResult DeleteUserInChat(int userId, int chatId)
        {
            var user = userService.GetUser(userId);
            var chat = chatService.GetChatById(chatId);
            var currentUser = userService.GetUserByName(User.FindFirst(ClaimTypes.Name).Value);

            var logUser = chat.ChatLogUsers.FirstOrDefault(l => l.User.Id == userId);
            logUser.StopChat = DateTime.Now;

            chat.Users.Remove(user);

            if (user.Id == currentUser.Id)
            {
                chatService.UpdateChat(chat);
                return RedirectToAction("Index", "Home");
            }
            if (chat.Users.Count == 0)
            {
                chatService.DeleteChat(chatId);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                chatService.UpdateChat(chat);
                return RedirectToAction("Index", new { idChat = chatId });
            }
        }

    }
}
