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


namespace ChatBotWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IChatService chatService;
        private readonly IUserService userService;
        public HomeController(IChatService chatService, IUserService userService)
        {
            this.chatService = chatService;
            this.userService = userService;
        }
        [Authorize]
        public ActionResult Index()
        {
            var chats = chatService.GetAllChats().OrderBy(c => c.ChatId);
            var currentUser = userService.GetUserByName(User.FindFirst(ClaimTypes.Name).Value);

            ViewData["NameUser"] = currentUser.Name;

            List<ChatViewModel> model = new List<ChatViewModel>();
            foreach (var chat in chats)
            {
                model.Add(new ChatViewModel()
                {
                    Id = chat.ChatId,
                    NameBots = userService.GetBotsByIdChat(chat.ChatId).Select(u => u.Name),
                    NameChat = chat.Name,
                    Users = userService.GetAllUsersByChat(chat.ChatId),
                });
            }

            return View("Index", model);
        }
        public ActionResult CreateChat()
        {
            return View("CreateChat");
        }
        [HttpPost]
        public ActionResult CreateChat(string chatName)
        {
            if (ModelState.IsValid)
            {
                var currentUser = userService.GetUserByName(User.FindFirst(ClaimTypes.Name).Value);
                Chat chat = new Chat(chatName);

                chat.ChatLogUsers.Add(new LogsUser() { StartChat = DateTime.Now, StopChat = null, User = currentUser });

                LogAction logAction = new LogAction(DateTime.Now, EventChat.CreateChat, currentUser);
                chat.LogActions.Add(logAction);
                chat.Users.Add(currentUser);

                chatService.CreateChat(chat);

                return RedirectToAction("Index", "Chat", new { IdChat = chat.ChatId });
            }

            return View();
        }
        [HttpPost]
        public ActionResult DeleteChat(int idChat)
        {
            chatService.DeleteChat(idChat);
            return RedirectToAction("Index");
        }
    }
}
