using ChatBotWeb.Service.ChatServices.Interface;
using Domian;
using Domian.Entities;
using System.Collections.Generic;

namespace ChatBotWeb.Service
{
    public class ChatServise : IChatService
    {
        IUnitOfWorck Database { get; set; }

        public ChatServise(IUnitOfWorck unit)
        {
            Database = unit;
        }

        public void CreateChat(Chat chat)
        {
            Database.Chats.Create(chat);
            Database.Save();

        }

        public void DeleteChat(int idChat)
        {
            Database.Chats.Delete(idChat);
            Database.Save();

        }

        public IEnumerable<Chat> GetAllChats()
        {
            return Database.Chats.GetAll();
        }


        public Chat GetChatById(int idChat)
        {
            return Database.Chats.Get(idChat);
        }
        public void UpdateChat(Chat chat)
        {
            Database.Save();
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
