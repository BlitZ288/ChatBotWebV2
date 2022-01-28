using System;

namespace Domian.Entities
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Content { get; set; }
        public DateTime dateTime { get; set; }
        public int? ChatId { get; set; }
        public User User { get; set; }
        public Chat Chat { get; set; }

        public Message(string content, User user)
        {
            Content = content;
            dateTime = DateTime.Now;
            User = user;
        }
        public Message()
        {

        }
    }
}
