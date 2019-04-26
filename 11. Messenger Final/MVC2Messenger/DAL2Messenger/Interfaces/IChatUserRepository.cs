using System.Collections.Generic;

namespace DAL2Messenger.Interfaces
{
    public interface IChatUserRepository : IBaseRepository<ChatUser>
    {
        List<ChatUser> GetChatUsers(int ChatId);
    }
}
