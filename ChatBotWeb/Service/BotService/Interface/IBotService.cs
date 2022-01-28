using Coman;

namespace ChatBotWeb.Service.BotService.Interface
{
    public interface IBotService
    {
        public void ProcessTextMessage(string contenMessage, int idChat);
        public void ProcessEvenInChat(EventChat eventChat, int idChat);


    }
}
