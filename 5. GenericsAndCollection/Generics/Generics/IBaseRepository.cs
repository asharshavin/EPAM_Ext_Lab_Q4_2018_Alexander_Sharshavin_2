﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics
{
    public interface IBaseRepository<T> where T : class, new()
    {
        T Get(int id);

        List<T> GetAll();

        bool Save(T entity);

        bool Delete(int id);
    }
}
