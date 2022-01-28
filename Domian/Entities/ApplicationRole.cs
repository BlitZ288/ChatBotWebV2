using Microsoft.AspNetCore.Identity;

namespace Domian.Entities
{
    public class ApplicationRole : IdentityRole<int>
    {
        public ApplicationRole(string name) : base(name)
        {

        }
    }
}
