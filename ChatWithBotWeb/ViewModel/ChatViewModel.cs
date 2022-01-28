using Domian.Entities;
using System.Collections.Generic;

namespace ChatWithBotWeb_v2.ViewModel
{
    public class ChatViewModel
    {
        public int Id { get; set; }
        public string NameChat { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<string> NameBots { get; set; }
    }
}
