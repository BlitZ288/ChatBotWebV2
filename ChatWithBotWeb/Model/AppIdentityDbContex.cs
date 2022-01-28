using Domian.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatWithBotWeb_v2.Model
{
    public class AppIdentityDbContex : IdentityDbContext<User, ApplicationRole, int>
    {
        public AppIdentityDbContex(DbContextOptions<AppIdentityDbContex> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);


            builder.Entity<User>().HasKey(p => p.Id);
            builder.Entity<User>().Property(p => p.Id).ValueGeneratedOnAdd();

        }
    }
}

