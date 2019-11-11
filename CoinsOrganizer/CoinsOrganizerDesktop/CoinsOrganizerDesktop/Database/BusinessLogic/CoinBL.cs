using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinsOrganizerDesktop.AllegroWebApiService;
using CoinsOrganizerDesktop.Database.DatabaseModels;
using CoinsOrganizerDesktop.Helpers;
using CoinsOrganizerDesktop.MarketService;

namespace CoinsOrganizerDesktop.Database.BusinessLogic
{
    public class CoinBL : BaseViewModel
    {
        private int _coinId;
        private string _name;
        private double _cost;
        private string _link;
        private string _polishName;
        private string _englishName;
        private double _zlotyPrice;
        private double _dollarPrice;
        private bool _isSold;
        private bool _isInStock;
        private string _aversFotoLink;
        private string _reversFotoLink;
        private int _orderId;
        private SellItemStruct _allegroItem;
        private int _allegroOnSaleCount;
        private OrderBL _orderBl;

        public CoinBL(Coin coin)
        {
            CoinDB = coin;
            //var order = coin.Order;

            //if (order != null && order.Count > 0)
            //{
            //    var lastOrder = order.LastOrDefault();
            //    OrderBL = new OrderBL(lastOrder) {CoinBL = this};
            //}

            UpdateAllegroData();

            TimerEvent.TimerFired+= TimerEventOnTimerFired;

        }

        private void TimerEventOnTimerFired(object sender, EventArgs e)
        {
            UpdateAllegroData();
        }

        public CoinBL(Coin coin, Order order)
        {
            CoinDB = coin;

            if (order != null)
            {
                OrderBL = new OrderBL(order);
            }

            UpdateAllegroData();
        }

        public void UpdateAllegroData()
        {
            _allegroItem = AllegroService.GetItemById(CoinDB.CoinId);
            _allegroOnSaleCount = AllegroService.GetItemCountById(CoinDB.CoinId);
            
            OnPropertyChanged(nameof(HasSuperfluousItemOnAllegroSale));
            OnPropertyChanged(nameof(IsOnAllegroSale));
            OnPropertyChanged(nameof(AllegroItemLink));
            OnPropertyChanged(nameof(AllegroItemEndTime));
            OnPropertyChanged(nameof(AllegroItemTitle));
            OnPropertyChanged(nameof(AllegroItemViewsCount));
            OnPropertyChanged(nameof(AllegroItemWatchersCount));
            OnPropertyChanged(nameof(AllegroItemBiddersCount));
            OnPropertyChanged(nameof(AllegroItemHighBidderLogin));
        }

        public Coin CoinDB { get; set; }

        public bool HasSuperfluousItemOnAllegroSale => _allegroOnSaleCount > 1;

        public bool IsOnAllegroSale => _allegroItem.itemId != -1;

        public string AllegroItemLink => "https://allegro.pl/oferta/" + _allegroItem.itemId;
        public string AllegroItemHighBidderUserLink => "https://allegro.pl/uzytkownik/" + AllegroItemHighBidderLogin;
        
        public TimeSpan AllegroItemEndTime =>
            (DateTimeOffset.FromUnixTimeSeconds(_allegroItem.itemEndTime).LocalDateTime - DateTime.Now);
        public DateTime AllegroItemEndTime2 => DateTimeOffset.FromUnixTimeSeconds(_allegroItem.itemEndTime).LocalDateTime; //new DateTime().AddSeconds(_allegroItem.itemEndTime).ToLocalTime();

        public string AllegroItemTitle => _allegroItem.itemTitle;

        public int AllegroItemViewsCount => _allegroItem.itemViewsCounter;

        public int AllegroItemWatchersCount => _allegroItem.itemWatchersCounter;

        public int AllegroItemBiddersCount => _allegroItem.itemBiddersCounter;

        public string AllegroItemInfo => string.Format("{3} {2} Of, {1} Obs, {0} Wiz, {4} {5}:{6}",
            AllegroItemViewsCount,
            AllegroItemWatchersCount, AllegroItemBiddersCount, AllegroItemHighBidderLogin,
            AllegroItemEndTime.Days.Equals(0) ? string.Empty : AllegroItemEndTime.Days + "дн",
            AllegroItemEndTime.Hours, AllegroItemEndTime.Minutes);

        public string AllegroItemHighBidderLogin => _allegroItem.itemHighestBidder?.userLogin;//_allegroItem.itemHighestBidder != null ? _allegroItem.itemHighestBidder.userLogin : string.Empty;

        public int CoinId
        {
            get => CoinDB.CoinId;
            private set => CoinDB.CoinId = value;
        }

        public string Name
        {
            get => CoinDB.Name;
            set => CoinDB.Name = value;
        }

        public double Cost
        {
            get => CoinDB.Cost;
            set => CoinDB.Cost = value;
        }

        public string Link  
        {
            get => CoinDB.Link;
            set => CoinDB.Link = value;
        }

        public string PolishName    
        {
            get => CoinDB.PolishName;
            set => CoinDB.PolishName = value;
        }

        public string EnglishName       
        {
            get => CoinDB.EnglishName;
            set => CoinDB.EnglishName = value;
        }

        public double ZlotyPrice    
        {
            get => CoinDB.ZlotyPrice;
            set
            {
                if (!value.Equals(-1))
                {
                    CoinDB.ZlotyPrice = value;
                }
            }
        }

        public double DollarPrice
        {
            get => CoinDB.DollarPrice;
            set
            {
                if (!value.Equals(-1))
                {
                    CoinDB.DollarPrice = value;
                }
            }
        }

        public bool IsSold
        {
            get => OrderBL != null && OrderBL.IsPaid;
            set => CoinDB.IsSold = value;
        }

        public bool IsInStock   
        {
            get => CoinDB.IsInStock;
            set => CoinDB.IsInStock = value;
        }

        public string AversFotoLink 
        {
            get => CoinDB.AversFotoLink;
            set => CoinDB.AversFotoLink = value;
        }

        public string ReversFotoLink    
        {
            get => CoinDB.ReversFotoLink;
            set => CoinDB.ReversFotoLink = value;
        }

        public OrderBL OrderBL
        {
            get
            {
                //if (_orderBl == null)
                //{
                //    var lastOrder = CoinDB.Order.LastOrDefault();
                //    if (lastOrder != null)
                //    {
                //        //_orderBl = new OrderBL(lastOrder);
                //    }
                //}

                return _orderBl;

            }
            set { _orderBl = value; }
        }

        public int OrderId  
        {
            get
            {
                var order = CoinDB.Order.FirstOrDefault(x => x.IsPaid);
                if (order != null)
                {
                    return order.OrderId;
                }

                return -1; 
                
            }
        }
    }
}