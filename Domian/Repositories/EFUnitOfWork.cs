using Domian.Context;
using Domian.Entities;
using Domian.Interfaces;
using System;

namespace Domian.Repositories
{
    public class EFUnitOfWork : IUnitOfWorck
    {
        private readonly ApplicationDbContext context;

        private UserRepository userRepository;
        private ChatRepository chatRepository;
        private LogActionRepository logActionRepository;
        private LogsUserRepository logsUserRepository;
        private MessageRepository messageRepository;

        private bool disponsed;

        public EFUnitOfWork(ApplicationDbContext options)
        {
            context = options;
        }

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(context);
                return userRepository;
            }
        }

        public IRepository<Chat> Chats
        {
            get
            {
                if (chatRepository == null)
                    chatRepository = new ChatRepository(context);
                return chatRepository;
            }
        }

        public IRepository<LogAction> LogActions
        {
            get
            {
                if (logActionRepository == null)
                    logActionRepository = new LogActionRepository(context);
                return logActionRepository;
            }
        }

        public IRepository<LogsUser> LogsUsers
        {
            get
            {
                if (logsUserRepository == null)
                    logsUserRepository = new LogsUserRepository(context);
                return logsUserRepository;
            }
        }

        public IRepository<Message> Message
        {
            get
            {
                if (messageRepository == null)
                    messageRepository = new MessageRepository(context);
                return messageRepository;
            }
        }

        public virtual void Dispose(bool disponsing)
        {
            if (!this.disponsed)
            {
                if (disponsing)
                {
                    context.Dispose();
                }
                this.disponsed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);///Не увер что нужно 
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
