using Domian.Entities;
using System.Collections.Generic;

namespace ChatBotWeb.Service.ChatServices.Interface
{
    public interface IChatService
    {
        IEnumerable<Chat> GetAllChats();
        Chat GetChatById(int idChat);

        void CreateChat(Chat chat);
        void DeleteChat(int idChat);

        void UpdateChat(Chat chat);
        void Dispose();
    }
}
