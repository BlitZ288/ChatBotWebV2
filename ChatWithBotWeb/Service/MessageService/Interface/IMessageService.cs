using Domian.Entities;
using System.Collections.Generic;

namespace ChatWithBotWeb_v2.Service
{
    public interface IMessageService
    {
        IEnumerable<Message> GetAllMessages();
        IEnumerable<Message> GetMessagesByChat(int chatId);
        IEnumerable<Message> HistoryMessagesForChat(int idChat, int idUser);
        Message GetMessageById(int idMessage);

        void CreateMessage(string content, User user, int chatId);
        void DeleteMessage(int idMessage);

        void Dispose();
    }
}
