using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;

namespace DataAccessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private Context DataBase { get; }
        private CoinRepository coinsRepository;

        public UnitOfWork()
        {
            DataBase = new Context();
        }

        public IRepository<Coin> Coins
        {
            get
            {
                if (coinsRepository == null)
                    coinsRepository = new CoinRepository(DataBase);
                return coinsRepository;
            }
        }
        public void Save()
        {
            DataBase.SaveChanges();
        }

        public void Dispose()
        {
            DataBase.Dispose();
        }
    }
}
