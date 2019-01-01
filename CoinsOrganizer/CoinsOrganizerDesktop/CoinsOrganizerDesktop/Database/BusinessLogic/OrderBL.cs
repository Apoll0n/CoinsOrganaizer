using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinsOrganizerDesktop.Database.DatabaseModels;
using CoinsOrganizerDesktop.Helpers;
using CoinsOrganizerDesktop.ViewModels.Model;

namespace CoinsOrganizerDesktop.Database.BusinessLogic
{
    public class OrderBL : BaseViewModel
    {
        private int _orderId;
        private string _name;
        private string _nickName;
        private string _email;
        private double _salePrice;
        private string _trackNumber;
        private string _orderDetails;
        private string _whereSold;
        private string _saleCurrency;
        private string _link;
        private bool _isPaid;
        private int _coinId;

        public OrderBL(Order order)
        {
            OrderDB = order;
        }

        public Order OrderDB { get; set; }

        public int OrderId
        {
            get { return OrderDB.OrderId; }
        }

        public string Name
        {
            get { return OrderDB.Name; }
            set { OrderDB.Name = value; }
        }

        public string NickName
        {
            get { return OrderDB.NickName; }
            set { OrderDB.NickName = value; }
        }

        public string Email
        {
            get { return OrderDB.Email; }
            set { OrderDB.Email = value; }
        }

        public double SalePrice
        {
            get { return OrderDB.SalePrice; }
            set { OrderDB.SalePrice = value; }
        }

        public string SalePriceAndCurrency
        {
            get { return string.Format("{0} {1}", OrderDB.SalePrice, SaleCurrency); }
        }


        public string SellerInformation
        {
            get { return string.Format(@"{0}, {1}, {2}", NickName, Email, OrderDetails); } //OrderDetails; }
        }

        public bool IsShipped
        {
            get { return OrderDB.IsShipped; }
            set
            {
                OrderDB.IsShipped = value;

                OnPropertyChanged(nameof(IsReadyForShipment));
                OnPropertyChanged(nameof(IsCompleted));
                OnPropertyChanged(nameof(HaveNotTrackedYetOnMarket));
                OnPropertyChanged(nameof(IsShipped));

                if (value)
                {
                    CoinBL.IsInStock = false;

                    if (IsPaid)
                    {
                        CoinBL.IsSold = true;
                    }
                }
            }
        }

        public bool IsTrackedOnMarket
        {
            get { return OrderDB.IsTrackedOnMarket; }
            set
            {
                OrderDB.IsTrackedOnMarket = value;

                OnPropertyChanged(nameof(IsReadyForShipment));
                OnPropertyChanged(nameof(IsCompleted));
                OnPropertyChanged(nameof(HaveNotTrackedYetOnMarket));
                OnPropertyChanged(nameof(IsShipped));
            }
        }

        public bool IsReadyForShipment
        {
            get { return IsPaid && !IsTrackedOnMarket && !IsShipped; }
        }

        public bool IsCompleted
        {
            get { return IsPaid && IsTrackedOnMarket && IsShipped; }
        }

        public bool HaveNotTrackedYetOnMarket
        {
            get { return IsPaid && !IsTrackedOnMarket && IsShipped; }
        }

        public string TrackNumber
        {
            get { return OrderDB.TrackNumber; }
            set { OrderDB.TrackNumber = value; }
        }

        public string OrderDetails
        {
            get { return OrderDB.OrderDetails; }
            set { OrderDB.OrderDetails = value; }
        }

        public WhereSold WhereSold
        {
            get
            {
                Enum.TryParse(OrderDB.WhereSold, out WhereSold myStatus);
                return myStatus; 
                
            }
            set
            {
                OrderDB.WhereSold = value.ToString(); 
                
            }
        }

        public string SaleCurrency
        {
            get { return OrderDB.SaleCurrency; }
            set { OrderDB.SaleCurrency = value; }
        }

        public string PriceCurrency
        {
            get { return string.Format("{0}{1}", SalePrice, SaleCurrency); }
        }

        public string Link
        {
            get { return OrderDB.Link; }
            set { OrderDB.Link = value; }
        }

        public bool IsPaid
        {
            get { return OrderDB.IsPaid; }
            set
            {
                OrderDB.IsPaid = value;

                OnPropertyChanged(nameof(IsPaid));
                OnPropertyChanged(nameof(IsReadyForShipment));
                OnPropertyChanged(nameof(IsCompleted));
                OnPropertyChanged(nameof(HaveNotTrackedYetOnMarket));
                OnPropertyChanged(nameof(IsShipped));

                if (value && IsShipped)
                {
                    CoinBL.IsSold = true;
                }
            }
        }

        public int CoinId
        {
            get { return OrderDB.CoinId; }
            private set { OrderDB.CoinId = value; }
        }

        public CoinBL CoinBL { get; set; }
    }
}
