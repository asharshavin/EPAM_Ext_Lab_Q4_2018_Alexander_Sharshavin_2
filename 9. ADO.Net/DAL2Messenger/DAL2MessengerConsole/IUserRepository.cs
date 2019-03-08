using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL2Messenger
{
    public interface IUserRepository : IBaseRepository<User>
    {
        List<User> GetAllUsers(int top);
    }
}
