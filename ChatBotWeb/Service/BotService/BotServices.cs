using ChatBotWeb.Service.BotService.Interface;
using ChatBotWeb.Service.UserService.Interface;
using Coman;
using Coman.Extensions;
using Coman.InterfaceBots;
using Domian.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBotWeb.Service.BotService
{
    public class BotServices : IBotService
    {
        private readonly IUserService userService;
        private readonly IMessageService messageService;

        private IEnumerable<IMessageBot> messageBots;
        private IEnumerable<IEventBot> eventBots;
        private IEnumerable<string> collectionNameBots;
        private readonly IBotsRepository botsRepository;
        private readonly IServiceScopeFactory serviceScope;

        private bool isRunning;

        public BotServices(IUserService userService, IMessageService messageService, IServiceScopeFactory serviceScopeFactory, IBotsRepository botsRepository = null)
        {
            this.botsRepository = botsRepository ?? new BotsRepository();

            this.serviceScope = serviceScopeFactory;
            this.messageService = messageService;
            this.userService = userService;

            Start();
        }

        private void Start()
        {
            if (isRunning)
            {
                throw new Exception("Вы пытаетесь повторно запустить BotsManager");
            }

            var eventBotList = new List<IEventBot>();
            var messageBotList = new List<IMessageBot>();
            var listName = new List<string>();

            var bots = botsRepository.GetAllBots();
            foreach (var bot in bots)
            {
                var botType = bot.GetType();
                if (botType.IsInterfaceImplemented(nameof(IEventBot)))
                {
                    eventBotList.Add((IEventBot)bot);
                }
                else if (botType.IsInterfaceImplemented(nameof(IMessageBot)))
                {
                    messageBotList.Add((IMessageBot)bot);
                }

                listName.Add(botType.Name);
            }

            this.messageBots = messageBotList;
            this.eventBots = eventBotList;
            this.collectionNameBots = listName;

            CheckBotsInDataBase();

            isRunning = true;
        }
        private void CheckBotsInDataBase()
        {
            var botNameInDatabase = userService.GetAllBots().Select(u => u.Name);

            var availableNameBot = collectionNameBots.Except(botNameInDatabase);

            if (availableNameBot.Any())
            {
                foreach (var name in availableNameBot)
                {
                    User user = new User(name);
                    user.TypeUser = TypeUser.Bot;

                    userService.CreateUserAsync(user);
                }
            }
        }

        public void ProcessEvenInChat(EventChat eventChat, int idChat)
        {
            using (var scope = serviceScope.CreateScope())
            {
                var serviceUser = scope.ServiceProvider.GetRequiredService<IUserService>();
                var serviceMessage = scope.ServiceProvider.GetRequiredService<IMessageService>();

                var nameBotsInChat = serviceUser.GetBotsByIdChat(idChat).Select(u => u.Name);

                var availableBotsName = this.eventBots.Where(b => nameBotsInChat.Contains(b.NameBot));

                if (!availableBotsName.Any())
                    return;

                var botsTasks = new List<Task<string>>();

                foreach (var eventBot in this.eventBots)
                {
                    botsTasks.Add(Task.Run(() => eventBot.Move(eventChat)));
                }

                int indexTask = Task.WaitAny(botsTasks.ToArray());
                var contentAnswer = botsTasks[indexTask].Result;

                if (String.IsNullOrEmpty(contentAnswer)) return;

                User user = serviceUser.GetUserByName(eventBots.ElementAt(indexTask).NameBot);

                serviceMessage.CreateMessage(contentAnswer, user, idChat);
            }
        }

        public void ProcessTextMessage(string contenMessage, int idChat)
        {
            using (var scope = serviceScope.CreateScope())
            {
                var serviceUser = scope.ServiceProvider.GetRequiredService<IUserService>();
                var serviceMessage = scope.ServiceProvider.GetRequiredService<IMessageService>();

                var nameBotsInChat = serviceUser.GetBotsByIdChat(idChat).Select(u => u.Name);
                var availableBotsName = this.messageBots.Where(b => nameBotsInChat.Contains(b.NameBot));

                if (!availableBotsName.Any())
                    return;

                var botsTasks = new List<Task<string>>();

                foreach (var messageBot in this.messageBots)
                {
                    botsTasks.Add(Task.Run(() => messageBot.Move(contenMessage)));
                }

                int indexTask = Task.WaitAny(botsTasks.ToArray());
                var contentAnswer = botsTasks[indexTask].Result;

                if (String.IsNullOrEmpty(contentAnswer)) return;

                User user = serviceUser.GetUserByName(messageBots.ElementAt(indexTask).NameBot);

                serviceMessage.CreateMessage(contentAnswer, user, idChat);
            }
        }
    }
}
