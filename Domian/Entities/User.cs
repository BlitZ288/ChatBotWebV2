using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Domian.Entities
{
    public class User : IdentityUser<int>
    {
        //public int IdUser { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int? ChatId { get; set; }
        public ICollection<Chat> Chats { get; set; }
        public TypeUser TypeUser { get; set; }
        public User() { }
        public User(string name)
        {
            this.Name = name;
            this.UserName = name;
        }
        public User(string name, string password)
        {
            Name = name;
            Password = password;
        }
        public override bool Equals(object obj)
        {
            return (obj == null) || (obj.GetType() != typeof(User))
            ? false
            : Name == ((User)obj).Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }


    }
}
