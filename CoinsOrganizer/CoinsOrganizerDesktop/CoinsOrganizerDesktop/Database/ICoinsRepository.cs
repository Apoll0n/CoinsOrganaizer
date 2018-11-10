using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinsOrganizerDesktop.Database
{
    public interface ICoinsRepository<T> where T : class
    {
        IEnumerable<T> ReadAll();
        T Read(int id);
        void Create(T value);
        void Update(T value);
        void Delete(int id);
    }
}
