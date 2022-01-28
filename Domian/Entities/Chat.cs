using System;
using System.Collections.Generic;

namespace Domian.Entities
{
    public class Chat
    {
        public int ChatId { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();

        public ICollection<Message> ListMessage { get; set; } = new List<Message>();

        public ICollection<LogAction> LogActions { get; set; } = new List<LogAction>();

        public ICollection<LogsUser> ChatLogUsers { get; set; } = new List<LogsUser>();

        public Chat(User user, string name)
        {
            Name = name;
            Users.Add(user);
            ChatLogUsers.Add(new LogsUser() { StartChat = DateTime.Now, StopChat = null });
        }
        public Chat(string name)
        {
            this.Name = name;
        }
        public Chat()
        {
        }

    }
}
