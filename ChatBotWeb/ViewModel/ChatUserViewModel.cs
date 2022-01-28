using Domian.Entities;
using System.Collections.Generic;

namespace ChatBotWeb.ViewModel
{
    public class ChatUserViewModel
    {
        public int ChatId { get; set; }
        public User CurrentUser { get; set; }
        public IEnumerable<User> UsersNotInclude { get; set; }
        public IEnumerable<User> ChatUsers { get; set; }
        public IEnumerable<Message> HistoryChat { get; set; }
        public IEnumerable<User> AvailableBots { get; set; }
        public IEnumerable<User> ChatBots { get; set; }
    }
}
