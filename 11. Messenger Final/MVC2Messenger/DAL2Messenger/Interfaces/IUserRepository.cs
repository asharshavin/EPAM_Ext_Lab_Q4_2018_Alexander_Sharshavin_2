using System.Collections.Generic;

namespace DAL2Messenger.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        List<User> GetAllUsers(int top);
    }
}
