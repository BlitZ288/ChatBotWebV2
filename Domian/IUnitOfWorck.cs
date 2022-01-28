using Domian.Entities;
using Domian.Interfaces;
using System;

namespace Domian
{
    public interface IUnitOfWorck : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Chat> Chats { get; }
        IRepository<LogAction> LogActions { get; }
        IRepository<LogsUser> LogsUsers { get; }
        IRepository<Message> Message { get; }
        void Save();
    }
}
