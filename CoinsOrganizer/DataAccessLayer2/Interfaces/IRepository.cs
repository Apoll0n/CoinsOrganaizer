using System.Collections.Generic;

namespace DataAccessLayer2.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> ReadAll();
        T Read(int id);
        void Create(T value);
        void Update(T value);
        void Delete(int id);
    }
}
