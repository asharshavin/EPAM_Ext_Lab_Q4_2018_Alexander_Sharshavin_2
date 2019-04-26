using System.Collections.Generic;

namespace DAL2Messenger.Interfaces
{
    public interface IMessageRepository : IBaseRepository<Message>
    {
        List<Message> GetChatMessages(int ChatId, int top);
    }
}
