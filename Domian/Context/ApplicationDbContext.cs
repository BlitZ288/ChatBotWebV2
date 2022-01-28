using Domian.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domian.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<LogsUser> LogsUsers { get; set; }
        public DbSet<LogAction> LogActions { get; set; }
    }
}
