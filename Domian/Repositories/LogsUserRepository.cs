using Domian.Context;
using Domian.Entities;
using Domian.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domian.Repositories
{
    class LogsUserRepository : IRepository<LogsUser>
    {
        private readonly ApplicationDbContext context;
        public LogsUserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Create(LogsUser item)
        {
            context.LogsUsers.Add(item);
        }

        public void Delete(int id)
        {
            var logUser = context.LogsUsers.Find(id);
            if (logUser != null)
            {
                context.LogsUsers.Remove(logUser);
            }
        }

        public IEnumerable<LogsUser> Find(Func<LogsUser, bool> predicat)
        {
            return context.LogsUsers.Include(l => l.Chat).Include(l => l.User).Where(predicat).ToList();
        }

        public LogsUser Get(int id)
        {
            return context.LogsUsers.Include(l => l.Chat).Include(l => l.User).FirstOrDefault(l => l.LogsUserId == id);
        }

        public IEnumerable<LogsUser> GetAll()
        {
            return context.LogsUsers.Include(l => l.Chat).Include(l => l.User);
        }

        public void Update(LogsUser item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }
}
