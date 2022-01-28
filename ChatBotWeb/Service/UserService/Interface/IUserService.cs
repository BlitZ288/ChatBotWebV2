using Domian.Entities;
using System.Collections.Generic;

namespace ChatBotWeb.Service.UserService.Interface
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUser();
        IEnumerable<User> GetAllUsersByChat(int chatId);
        IEnumerable<User> GetAllBots();
        IEnumerable<User> GetBotsByIdChat(int chatId);

        User GetUser(int id);
        User GetUserByName(string name);

        public void Save();
        void CreateUserAsync(User user);
        void Dispose();
    }
}
