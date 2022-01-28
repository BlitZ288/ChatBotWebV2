using Domian.Context;
using Domian.Entities;
using Domian.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domian.Repositories
{
    class ChatRepository : IRepository<Chat>
    {
        private readonly ApplicationDbContext context;
        public ChatRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Create(Chat item)
        {
            context.Chats.Add(item);
        }

        public void Delete(int id)
        {
            var chat = Get(id);
            if (chat != null)
            {
                context.Chats.Remove(chat);
            }
        }

        public IEnumerable<Chat> Find(Func<Chat, bool> predicat)
        {
            return context.Chats.Include(c => c.Users).Include(c => c.ListMessage).Include(c => c.LogActions).Include(c => c.ChatLogUsers).Where(predicat).ToList();
        }

        public Chat Get(int id)
        {
            return context.Chats.Include(c => c.Users).Include(c => c.ListMessage).Include(c => c.LogActions).Include(c => c.ChatLogUsers).FirstOrDefault(c => c.ChatId == id);
        }

        public IEnumerable<Chat> GetAll()
        {
            return context.Chats.Include(c => c.Users).Include(c => c.ListMessage).Include(c => c.LogActions).Include(c => c.ChatLogUsers);
        }

        public void Update(Chat item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }
}
