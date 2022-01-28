using System;
using System.Collections.Generic;

namespace Domian.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        IEnumerable<T> Find(Func<T, bool> predicat);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
