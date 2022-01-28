using Domian.Entities;
using System.Collections.Generic;

namespace ChatBotWeb.ViewModel
{
    public class ChatViewModel
    {
        public int Id { get; set; }
        public string NameChat { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<string> NameBots { get; set; }
    }
}
