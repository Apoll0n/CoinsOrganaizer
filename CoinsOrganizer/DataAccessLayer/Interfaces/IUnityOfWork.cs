using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Coin> Coins { get; }
        IRepository<Order> Orders { get; }
        void Save();
    }
}