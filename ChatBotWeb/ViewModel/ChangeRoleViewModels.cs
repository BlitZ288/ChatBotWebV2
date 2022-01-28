using Domian.Entities;
using System.Collections.Generic;

namespace ChatBotWeb.ViewModel
{
    public class ChangeRoleViewModels
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public List<ApplicationRole> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }


    }
}
