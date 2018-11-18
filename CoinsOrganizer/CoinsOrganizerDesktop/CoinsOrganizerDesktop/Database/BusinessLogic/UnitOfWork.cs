using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using CoinsOrganizerDesktop.Database.DatabaseModels;
using CoinsOrganizerDesktop.DataBase.DbContext;
using eBay.Service.Core.Soap;

namespace CoinsOrganizerDesktop.Database.BusinessLogic
{
    public class UnitOfWork //: IUnitOfWork
    {
        private CoinsOrganizerContext DataBase { get; }
        private OrderDB _ordersRepository;
        private CoinsDB _coinsRepository;
        private ObservableCollection<CoinBL> _coinsBl;
        private ObservableCollection<OrderBL> _ordersBl;

        public UnitOfWork()
        {
            var connectionString = ConfigurationManager.AppSettings["DbConnectionString"];
            DataBase = new CoinsOrganizerContext(connectionString);

            var coinsBl = CoinsBl;
            var ordersBl = OrdersBl;

            foreach (var order in ordersBl)
            {
                var coin = coinsBl.Single(x => x.CoinId.Equals(order.CoinId));
                order.CoinBL = coin;
                coin.OrderBL = order;
            }
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

        public ObservableCollection<CoinBL> CoinsBl
        {
            get
            {
                if (_coinsBl == null)
                {
                    _coinsBl = new ObservableCollection<CoinBL>();
                    if (_coinsRepository == null)
                    {
                        _coinsRepository = new CoinsDB(DataBase);
                    }

                    var coins = _coinsRepository.ReadAll();

                    foreach (var item in coins)
                    {
                        var coinBl = new CoinBL(item);
                        _coinsBl.Add(coinBl);
                    }
                }
                return _coinsBl;
            }
        }

        public ObservableCollection<OrderBL> OrdersBl
        {
            get
            {
                if (_ordersBl == null)
                {
                    _ordersBl = new ObservableCollection<OrderBL>();
                    if (_ordersRepository == null)
                    {
                        _ordersRepository = new OrderDB(DataBase);
                    }

                    var orders = _ordersRepository.ReadAll();

                    foreach (var item in orders)
                    {
                        var orderBl = new OrderBL(item);
                        _ordersBl.Add(orderBl);
                    }
                }
                return _ordersBl;
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