using System;

namespace Domian.Entities
{
    public class LogsUser
    {
        public int LogsUserId { get; set; }
        public DateTime StartChat { get; set; }
        public DateTime? StopChat { get; set; }
        public int? ChatId { get; set; }
        public Chat Chat { get; set; }
        public User User { get; set; }
        //public int? IdUser { get; set; }

    }
}
