using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinsOrganizerDesktop.AllegroWebApiService;
using CoinsOrganizerDesktop.Database.DatabaseModels;
using CoinsOrganizerDesktop.MarketService;

namespace CoinsOrganizerDesktop.Database.BusinessLogic
{
    public class CoinBL
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

        public CoinBL(Coin coin)
        {
            CoinDB = coin;
            _allegroItem = AllegroService.GetItemById(coin.CoinId);
        }

        public Coin CoinDB { get; set; }

        public bool IsOnAllegroSale
        {
            get { return _allegroItem.itemId == -1; }
        }

        public string AllegroItemLink
        {
            get { return "https://allegro.pl/oferta/" + _allegroItem.itemId; }
        }

        public DateTime AllegroItemEndTime
        {
            get { return new DateTime(_allegroItem.itemEndTime); }
        }

        public string AllegroItemTitle
        {
            get { return _allegroItem.itemTitle; }
        }

        public int AllegroItemViewsCount
        {
            get { return _allegroItem.itemViewsCounter; }
        }

        public int AllegroItemWatchersCount
        {
            get { return _allegroItem.itemWatchersCounter; }
        }

        public int AllegroItemBiddersCount
        {
            get { return _allegroItem.itemBiddersCounter; }
        }

        public string AllegroItemHighBidderLogin
        {
            get { return _allegroItem.itemHighestBidder.userLogin; }
        }

        public int CoinId
        {
            get { return CoinDB.CoinId; }
            private set { CoinDB.CoinId = value; }
        }

        public string Name
        {
            get { return CoinDB.Name; }
            set { CoinDB.Name = value; }
        }

        public double Cost
        {
            get { return CoinDB.Cost; }
            private set { CoinDB.Cost = value; }
        }

        public string Link  
        {
            get { return CoinDB.Link; }
            set { CoinDB.Link = value; }
        }

        public string PolishName    
        {
            get { return CoinDB.PolishName; }
            set { CoinDB.PolishName = value; }
        }

        public string EnglishName       
        {
            get { return CoinDB.EnglishName; }
            set { CoinDB.EnglishName = value; }
        }

        public double ZlotyPrice    
        {
            get { return CoinDB.ZlotyPrice; }
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
            get { return CoinDB.DollarPrice; }
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
            get { return OrderBL != null && OrderBL.IsPaid; }
            set { CoinDB.IsSold = value; }
        }

        public bool IsInStock   
        {
            get { return CoinDB.IsInStock; }
            set { CoinDB.IsInStock = value; }
        }

        public string AversFotoLink 
        {
            get { return CoinDB.AversFotoLink; }
            set { CoinDB.AversFotoLink = value; }
        }

        public string ReversFotoLink    
        {
            get { return CoinDB.ReversFotoLink; }
            set { CoinDB.ReversFotoLink = value; }
        }

        public OrderBL OrderBL { get; set; }

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