using DataAccessLayer2.EF;
using DataAccessLayer2.Interfaces;
using DataAccessLayer2.Repositories;

namespace DataAccessLayer2
{
    public class UnitOfWork : IUnitOfWork
    {
        private Context DataBase { get; }
        private CoinRepository coinsRepository;
        private OrderRepository ordersRepository;

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

        public IRepository<Order> Orders
        {
            get
            {
                if (ordersRepository == null)
                    ordersRepository = new OrderRepository(DataBase);
                return ordersRepository;
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
