using System;
using DAL2Messenger.Interfaces;

namespace DAL2Messenger.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        IChatRepository Chats { get; }
        IMessageRepository Messages { get; }
        IChatUserRepository ChatUsers { get; }
        void Save();
    }
}
