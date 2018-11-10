using System.Configuration;
using CoinsOrganizerDesktop.Database.DatabaseModels;
using CoinsOrganizerDesktop.DataBase.DbContext;

namespace CoinsOrganizerDesktop.Database.BusinessLogic
{
    public class UnitOfWork //: IUnitOfWork
    {
        private CoinsOrganizerContext DataBase { get; }
        private OrderDB _ordersRepository;
        private CoinsDB _coinsRepository;

        public UnitOfWork()
        {
            var connectionString = ConfigurationManager.AppSettings["DbConnectionString"];
            DataBase = new CoinsOrganizerContext(connectionString);
        }

        public ICoinsRepository<Coin> Coins
        {
            get
            {
                if (_coinsRepository == null)
                    _coinsRepository = new CoinsDB(DataBase);
                return _coinsRepository;
            }
        }

        public ICoinsRepository<Order> Orders
        {
            get
            {
                if (_ordersRepository == null)
                    _ordersRepository = new OrderDB(DataBase);
                return _ordersRepository;
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