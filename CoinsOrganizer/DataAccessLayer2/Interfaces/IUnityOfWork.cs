using System;

namespace DataAccessLayer2.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Coin> Coins { get; }
        IRepository<Order> Orders { get; }
        void Save();
    }
}