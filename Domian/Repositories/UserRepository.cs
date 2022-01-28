using Domian.Context;
using Domian.Entities;
using Domian.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domian.Repositories
{
    class UserRepository : IRepository<User>
    {
        private readonly ApplicationDbContext context;
        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Create(User item)
        {
            context.Users.Add(item);
        }

        public void Delete(int id)
        {
            var user = context.Users.Find(id);
            if (user != null)
            {
                context.Users.Remove(user);
            }
        }

        public IEnumerable<User> Find(Func<User, bool> predicat)
        {
            return context.Users.Include(u => u.Chats).Where(predicat).ToList();
        }

        public User Get(int id)
        {
            return context.Users.Include(u => u.Chats).FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<User> GetAll()
        {
            return context.Users.Include(u => u.Chats);
        }

        public void Update(User item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }
}
