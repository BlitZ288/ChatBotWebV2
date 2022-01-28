using ChatBotWeb.Service.UserService.Interface;
using Domian;
using Domian.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace ChatBotWeb.Service.UserService
{
    public class UserSercvice : IUserService
    {
        IUnitOfWorck Database { get; set; }
        private readonly UserManager<User> userManager;

        public UserSercvice(IUnitOfWorck unit, UserManager<User> userManager)
        {
            Database = unit;
            this.userManager = userManager;
        }
        public void CreateUserAsync(User user)
        {
            var result = userManager.CreateAsync(user).Result;
            if (result.Succeeded)
            {
                Database.Users.Create(user);
                Database.Save();
            }
        }

        public IEnumerable<User> GetAllUser()
        {
            return Database.Users.GetAll().Where(u => u.TypeUser == TypeUser.Persone);
        }

        public IEnumerable<User> GetAllUsersByChat(int chatId)
        {
            return Database.Chats.Get(chatId).Users.Where(u => u.TypeUser == TypeUser.Persone);
        }
        public IEnumerable<User> GetAllBots()
        {
            return Database.Users.GetAll().Where(u => u.TypeUser == TypeUser.Bot);
        }

        public IEnumerable<User> GetBotsByIdChat(int chatId)
        {
            return Database.Chats.Get(chatId).Users.Where(u => u.TypeUser == TypeUser.Bot);
        }

        public User GetUser(int id)
        {
            return Database.Users.Get(id);
        }

        public User GetUserByName(string name)
        {
            return Database.Users.GetAll().FirstOrDefault(u => u.Name == name);
        }
        public void Save()
        {
            Database.Save();
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
