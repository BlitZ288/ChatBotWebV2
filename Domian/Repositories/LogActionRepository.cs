using Domian.Context;
using Domian.Entities;
using Domian.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domian.Repositories
{
    class LogActionRepository : IRepository<LogAction>
    {

        private readonly ApplicationDbContext context;
        public LogActionRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Create(LogAction item)
        {
            context.LogActions.Add(item);
        }

        public void Delete(int id)
        {
            var logAction = context.LogActions.Find(id);
            if (logAction != null)
            {
                context.LogActions.Remove(logAction);
            }
        }

        public IEnumerable<LogAction> Find(Func<LogAction, bool> predicat)
        {
            return context.LogActions.Include(l => l.Chat).Include(l => l.User).Where(predicat).ToList();
        }

        public LogAction Get(int id)
        {
            return context.LogActions.Include(l => l.Chat).Include(l => l.User).FirstOrDefault(l => l.LogActionId == id);
        }

        public IEnumerable<LogAction> GetAll()
        {
            return context.LogActions.Include(l => l.Chat).Include(l => l.User);
        }

        public void Update(LogAction item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }
}
