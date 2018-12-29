using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CoinsOrganizerDesktop.Database.BusinessLogic;
using CoinsOrganizerDesktop.Database.DatabaseModels;
using CoinsOrganizerDesktop.Helpers;
using CoinsOrganizerDesktop.ViewModels.Model;

namespace CoinsOrganizerDesktop.ViewModels
{
    public class OrdersViewModel : BaseViewModel
    {
        private BusinessLogic _businessLogic;
        private ObservableCollection<OrderBL> _orders;
        public OrderBL _selectedRow;

        public OrdersViewModel()
        {
            _businessLogic = new BusinessLogic();
            var ordersbl = _businessLogic.GetOrdersBL();
            Orders = new ObservableCollection<OrderBL>(ordersbl);

            NewOrder = new NewOrderModel();
        }

        public NewOrderModel NewOrder { get; set; }

        public bool SortByDescending { get; set; }

        public ICommand AddNewOrderCommand
        {
            get
            {
                return new ActionCommand(() =>
                {
                    var buyerInfos = NewOrder.BuyerInfo.Split(
                        new[] {Environment.NewLine},
                        StringSplitOptions.None);

                    string orderDetails = String.Empty;
                    string email = String.Empty;

                    foreach (var buyerInfo in buyerInfos)
                    {
                        if (orderDetails != String.Empty)
                        {
                            orderDetails += ", ";
                        }

                        orderDetails += buyerInfo;
                        if (buyerInfo.Contains("@"))
                        {
                            email = buyerInfo;
                        }
                    }

                    var order = new Order
                    {
                        
                        SaleCurrency = CurrencyHelper.ConvertToSign(NewOrder.Currency),
                        NickName = buyerInfos[0],
                        Email = email,
                        OrderDetails = orderDetails,
                        SalePrice = NewOrder.Price,
                        Name = NewOrder.Name,
                        Link = NewOrder.Link,
                        WhereSold = NewOrder.WhereSold.ToString(),
                        CoinId = int.Parse(NewOrder.CoinIndex)
                    };

                    _businessLogic.AddOrder(order);

                    _businessLogic.ApplyChanges();

                    OnPropertyChanged(nameof(Orders));
                    //ChangeTableState(TableState);
                    //SortTableIndex(SortByDescending);
                });
            }
        }

        public ICommand DeleteCoinCommand
        {
            get
            {
                return new ActionCommand<CoinBL>((e) =>
                {
                    //_businessLogic.RemoveCoin(((CoinBL)e).CoinId);
                    //_businessLogic.ApplyChanges();

                    //ChangeTableState(TableState);
                    //SortTableIndex(SortByDescending);
                });
            }
        }

        public ICommand ShowOnlyAvailableCoinsCommand
        {
            get
            {
                return new ActionCommand(() =>
                {
                    //TableState = CoinTableState.Available;
                    //ChangeTableState(TableState);
                    //SortTableIndex(SortByDescending);
                });
            }
        }

        public ICommand ShowAllCoinsCommand
        {
            get
            {
                return new ActionCommand(() =>
                {
                    //TableState = CoinTableState.All;
                    //ChangeTableState(TableState);
                    //SortTableIndex(SortByDescending);
                });
            }
        }

        public ICommand ShowSoldCoinsCommand
        {
            get
            {
                return new ActionCommand(() =>
                {
                    //TableState = CoinTableState.Sold;
                    //ChangeTableState(TableState);
                    //SortTableIndex(SortByDescending);
                });
            }
        }

        public ICommand SortCoinsCommand
        {
            get
            {
                return new ActionCommand<object>((e) =>
                {
                    //SortByDescending = (bool)e;
                    //SortTableIndex(SortByDescending);
                });
            }
        }

        public void SortTableIndex(bool byDescending)
        {
            //if (byDescending)
            //{
            //    Orders = new ObservableCollection<CoinBL>(Orders.OrderByDescending(x => x.CoinId));
            //}
            //else
            //{
            //    Orders = new ObservableCollection<CoinBL>(Orders.OrderBy(x => x.CoinId));
            //}
        }

        public void ChangeTableState(CoinTableState state)
        {
            //IEnumerable<CoinBL> coins = Enumerable.Empty<CoinBL>();

            //if (state == CoinTableState.All)
            //{
            //    coins = _businessLogic.GetCoinsBL();
            //}
            //else if (state == CoinTableState.Available)
            //{
            //    coins = _businessLogic.GetCoinsBL().Where(x => x.IsInStock);
            //}
            //else if (state == CoinTableState.Sold)
            //{
            //    coins = _businessLogic.GetCoinsBL().Where(x => x.IsSold);
            //}

            //Orders = new ObservableCollection<OrderBL>(coins);
        }

        public OrderBL SelectedRow
        {
            get
            {
                return _selectedRow;
            }
            set
            {
                _businessLogic.UpdateOrder(_selectedRow);
                _selectedRow = value;
            }
        }

        public ObservableCollection<OrderBL> Orders
        {
            get { return _orders; }
            set
            {
                _orders = value;

                OnPropertyChanged(nameof(Orders));
            }
        }
    }
}
