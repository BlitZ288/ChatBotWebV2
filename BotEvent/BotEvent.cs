using Coman;
using Coman.InterfaceBots;

namespace BotEvent
{
    public class BotEvent : IEventBot
    {
        public string NameBot => nameof(BotEvent);

        public string Move(EventChat command)
        {
            string result = "";
            switch (command)
            {
                case EventChat.JoinChat:
                    result = "Добро пожаловать в чат ";
                    break;
                case EventChat.DeletePerson:
                    result = "Пользователь вышел из чата ";
                    break;
                case EventChat.DeleteBot:
                    result = "Бот удален";
                    break;
            }
            return result;
        }
    }
}
