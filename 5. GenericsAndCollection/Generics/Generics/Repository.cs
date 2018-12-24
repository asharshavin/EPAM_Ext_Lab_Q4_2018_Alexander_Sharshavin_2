using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics
{
    public class Repository : IBaseRepository<User>
    {
        protected List<User> ListOfUser;

        public Repository()
        {
            ListOfUser = new List<User>();
        }

        public bool Delete(int id)
        {
            if (id < 0 || id > ListOfUser.Count - 1)
                return false;

            ListOfUser.RemoveAt(id);
            return true;
        }

        public User Get(int id)
        {
            return ListOfUser[id];
        }

        public List<User> GetAll()
        {
            return ListOfUser;
        }

        public bool Save(User entity)
        {
            ListOfUser.Add(entity);
            return true;
        }
    }
}
