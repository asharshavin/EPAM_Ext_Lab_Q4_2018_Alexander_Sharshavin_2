using System.Configuration;
using DAL2Messenger.Interfaces;
using DAL2Messenger;

namespace DAL2Messenger
{
    public class UnitOfWork : IUnitOfWork
    {
        private UserRepository userRepository;
        private RoleRepository roleRepository;
        private ChatRepository chatRepository;
        private MessageRepository messageRepository;
        private ChatUserRepository chatUsersRepository;

        public string connectionString;

        public UnitOfWork(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IUserRepository Users
        {
            get
            {
                if (userRepository == null)
                {
                    var connectionStringItem = ConfigurationManager.ConnectionStrings[connectionString];
                    userRepository = new UserRepository(connectionStringItem);
                }
                return userRepository;
            }
        }

        public IRoleRepository Roles
        {
            get
            {
                if (roleRepository == null)
                {
                    var connectionStringItem = ConfigurationManager.ConnectionStrings[connectionString];
                    roleRepository = new RoleRepository(connectionStringItem);
                }
                return roleRepository;
            }
        }

        public IChatRepository Chats
        {
            get
            {
                if (chatRepository == null)
                {
                    var connectionStringItem = ConfigurationManager.ConnectionStrings[connectionString];
                    chatRepository = new ChatRepository(connectionStringItem);
                }
                return chatRepository;
            }
        }

        public IMessageRepository Messages
        {
            get
            {
                if (messageRepository == null)
                {
                    var connectionStringItem = ConfigurationManager.ConnectionStrings[connectionString];
                    messageRepository = new MessageRepository(connectionStringItem);
                }
                return messageRepository;
            }
        }

        public IChatUserRepository  ChatUsers
        {
            get
            {
                if (chatUsersRepository == null)
                {
                    var connectionStringItem = ConfigurationManager.ConnectionStrings[connectionString];
                    chatUsersRepository = new ChatUserRepository(connectionStringItem);
                }
                return chatUsersRepository;
            }
        }

        public void Save()
        {
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}