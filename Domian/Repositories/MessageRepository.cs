using Domian.Context;
using Domian.Entities;
using Domian.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domian.Repositories
{
    class MessageRepository : IRepository<Message>
    {
        private readonly ApplicationDbContext context;
        public MessageRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Create(Message item)
        {
            context.Messages.Add(item);
        }

        public void Delete(int id)
        {
            var message = context.Messages.Find(id);
            if (message != null)
            {
                context.Messages.Remove(message);
            }
        }
        public void Update(Message item)
        {
            context.Entry(item).State = EntityState.Modified;
        }

        public IEnumerable<Message> Find(Func<Message, bool> predicat)
        {
            return context.Messages.Include(m => m.Chat).Include(m => m.User).Where(predicat).ToList();
        }

        public Message Get(int id)
        {
            return context.Messages.Include(m => m.Chat).Include(m => m.User).FirstOrDefault(m => m.MessageId == id);
        }

        public IEnumerable<Message> GetAll()
        {
            return context.Messages.Include(m => m.Chat).Include(m => m.User);
        }
    }
}
