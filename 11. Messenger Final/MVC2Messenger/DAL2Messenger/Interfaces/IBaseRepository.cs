using System.Collections.Generic;

namespace DAL2Messenger.Interfaces
{
    public interface IBaseRepository<T> where T : class, new()
    {
        T Get(int id);

        List<T> GetAll();

        bool Save(T entity);

        bool Delete(int id);
    }
}
