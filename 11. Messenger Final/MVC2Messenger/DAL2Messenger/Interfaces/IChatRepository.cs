using System.Collections.Generic;

namespace DAL2Messenger.Interfaces
{
    public interface IChatRepository : IBaseRepository<Chat>
    {
        List<Chat> GetAllChatUser(int UserId);
    }
}
