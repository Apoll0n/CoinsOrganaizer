using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
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
        private IEnumerable<OrderBL> _orders;
        public OrderBL _selectedRow;
        private OrdersFilters _selectedOrderFilter;

        public OrdersViewModel()
        {
            _businessLogic = new BusinessLogic();
            var ordersbl = _businessLogic.GetOrdersBL();
            Orders = new ObservableCollection<OrderBL>(ordersbl);

            NewOrder = new NewOrderModel();

            List<OrdersFilters> items = new List<OrdersFilters>();
            items.Add(new OrdersFilters() {Name = "Всі", Category = "A"});
            items.Add(new OrdersFilters() {Name = "Оплачені", Category = "B"});
            items.Add(new OrdersFilters() {Name = "Не оплачені", Category = "B"});
            items.Add(new OrdersFilters() {Name = "Відправлені", Category = "C"});
            items.Add(new OrdersFilters() {Name = "Не відправлені", Category = "C"});
            items.Add(new OrdersFilters() {Name = "Оплачені, не відправлені", Category = "C"});
            items.Add(new OrdersFilters() {Name = "Трек номер не вказаний", Category = "C"});
            items.Add(new OrdersFilters() {Name = "Продано на eBay", Category = "D"});
            items.Add(new OrdersFilters() {Name = "Продано на Allegro", Category = "D"});
            items.Add(new OrdersFilters() {Name = "Продано іншим чином", Category = "D"});

            ListCollectionView groupedFilters = new ListCollectionView(items);
            groupedFilters.GroupDescriptions.Add(new PropertyGroupDescription("Category"));
            OrdersFiltersSource = groupedFilters;
        }

        public OrdersFilters SelectedOrderFilter
        {
            get { return _selectedOrderFilter; }
            set
            {
                _selectedOrderFilter = value;
                FilterOrders(value);
            }
        }

        private void FilterOrders(OrdersFilters filter)
        {
            IEnumerable<OrderBL> orders = Enumerable.Empty<OrderBL>();

            switch (filter.Name)
            {
                case "Всі":

                    orders = _businessLogic.GetOrdersBL();
                    Orders = new ObservableCollection<OrderBL>(orders);

                    break;
                case "Оплачені":

                    orders = _businessLogic.GetOrdersBL().Where(x => x.IsPaid);
                    Orders = new ObservableCollection<OrderBL>(orders);

                    break;
                case "Не оплачені":

                    orders = _businessLogic.GetOrdersBL().Where(x => !x.IsPaid);
                    Orders = new ObservableCollection<OrderBL>(orders);

                    break;
                case "Відправлені":

                    orders = _businessLogic.GetOrdersBL().Where(x => x.IsShipped);
                    Orders = new ObservableCollection<OrderBL>(orders);

                    break;
                case "Не відправлені":

                    orders = _businessLogic.GetOrdersBL().Where(x => !x.IsShipped);
                    Orders = new ObservableCollection<OrderBL>(orders);

                    break;
                case "Оплачені, не відправлені":

                    orders = _businessLogic.GetOrdersBL().Where(x => x.IsPaid && !x.IsShipped);
                    Orders = new ObservableCollection<OrderBL>(orders);

                    break;
                case "Трек номер не вказаний":

                    orders = _businessLogic.GetOrdersBL().Where(x => x.IsTrackedOnMarket);
                    Orders = new ObservableCollection<OrderBL>(orders);

                    break;
                case "Продано на eBay":

                    orders = _businessLogic.GetOrdersBL().Where(x => x.WhereSold == WhereSold.Ebay);
                    Orders = new ObservableCollection<OrderBL>(orders);

                    break;
                case "Продано на Allegro":

                    orders = _businessLogic.GetOrdersBL().Where(x => x.WhereSold == WhereSold.Allegro);
                    Orders = new ObservableCollection<OrderBL>(orders);

                    break;
                case "Продано іншим чином":

                    orders = _businessLogic.GetOrdersBL().Where(x => x.WhereSold == WhereSold.Інше);
                    Orders = new ObservableCollection<OrderBL>(orders);

                    break;
                default: break;
            } 
        }

        public ListCollectionView OrdersFiltersSource { get; set; }

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
            get { return _selectedRow; }
            set
            {
                _businessLogic.UpdateOrder(_selectedRow);
                _selectedRow = value;
            }
        }

        public IEnumerable<OrderBL> Orders
        {
            get { return _orders; }
            set
            {
                _orders = value;

                OnPropertyChanged(nameof(Orders));
            }
        }
    }

    public class OrdersFilters
    {
        public string Name { get; set; }
        public string Category { get; set; }
    }
}
