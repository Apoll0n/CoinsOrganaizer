using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        private ICollectionView _orders;
        public OrderBL _selectedRow;
        private ItemsFilters _selectedItemFilter;

        public OrdersViewModel()
        {
            _businessLogic = new BusinessLogic();
            var ordersbl = _businessLogic.GetOrdersBL();

            NewOrder = new NewOrderModel();

            List<ItemsFilters> items = new List<ItemsFilters>();
            items.Add(new ItemsFilters() {Name = "Всі", Category = "A"});
            items.Add(new ItemsFilters() {Name = "Оплачені", Category = "B"});
            items.Add(new ItemsFilters() {Name = "Не оплачені", Category = "B"});
            items.Add(new ItemsFilters() {Name = "Відправлені", Category = "C"});
            items.Add(new ItemsFilters() {Name = "Не відправлені", Category = "C"});
            items.Add(new ItemsFilters() {Name = "Оплачені, не відправлені", Category = "C"});
            items.Add(new ItemsFilters() {Name = "Трек номер не вказаний", Category = "C"});
            items.Add(new ItemsFilters() {Name = "Продано на eBay", Category = "D"});
            items.Add(new ItemsFilters() {Name = "Продано на Allegro", Category = "D"});
            items.Add(new ItemsFilters() {Name = "Продано іншим чином", Category = "D"});

            ICollectionView groupedFilters = new ListCollectionView(items);
            groupedFilters.GroupDescriptions.Add(new PropertyGroupDescription("Category"));
            OrdersFiltersSource = groupedFilters;


            ICollectionView collection = new ListCollectionView(ordersbl);
            //collection.GroupDescriptions.Add(new PropertyGroupDescription("NickName1"));
            Orders = collection;

            SelectedItemFilter = items[0];
            collection.Filter = OrderFilter;
        }

        public ItemsFilters SelectedItemFilter
        {
            get { return _selectedItemFilter; }
            set
            {
                _selectedItemFilter = value;
                Orders.Refresh();
            }
        }

        private bool OrderFilter(object obj)
        {
            bool result = true;

            var order = (OrderBL)obj;
            if (!order.IsPaid)
            {
                
            }

            switch (SelectedItemFilter.Name)
            {
                case "Оплачені":

                    result = order.IsPaid && !order.IsIgnored;

                    break;
                case "Не оплачені":

                    result = !order.IsPaid && !order.IsIgnored;

                    break;
                case "Відправлені":

                    result = order.IsShipped && !order.IsIgnored;

                    break;
                case "Не відправлені":

                    result = !order.IsShipped && !order.IsIgnored;

                    break;
                case "Оплачені, не відправлені":

                    result = order.IsPaid && !order.IsShipped && !order.IsIgnored;

                    break;
                case "Трек номер не вказаний":

                    result = !order.IsTrackedOnMarket && !order.IsIgnored;

                    break;
                case "Продано на eBay":

                    result = order.WhereSold == WhereSold.Ebay;

                    break;
                case "Продано на Allegro":

                    result = order.WhereSold == WhereSold.Allegro;

                    break;
                case "Продано іншим чином":

                    result = order.WhereSold == WhereSold.Інше;

                    break;
                default: break;
            }
            return result;
        }

        public ICollectionView OrdersFiltersSource { get; set; }

        public NewOrderModel NewOrder { get; set; }

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
                });
            }
        }

        public ICommand DeleteOrderCommand
        {
            get
            {
                return new ActionCommand<CoinBL>((e) =>
                {
                });
            }
        }

        public ICommand GroupOrdersCommand
        {
            get
            {
                return new ActionCommand<object>((e) =>
                {
                    //var list = _businessLogic.GetOrdersBL();
                    //var groupSumsQuery = from model in list
                    //    group model by model.NickName
                    //    into modelGroup
                    //    select new
                    //    {
                    //        Group = modelGroup,
                    //        Name = modelGroup.Key,
                    //        Sum = modelGroup.Sum(model => model.SalePrice)
                    //    };

                    if ((bool) e)
                    {
                        Orders.GroupDescriptions.Add(new PropertyGroupDescription("NickName"));
                    }
                    else
                    {
                        Orders.GroupDescriptions.Clear();
                    }

                });
            }
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

        public ICollectionView Orders
        {
            get { return _orders; }
            set
            {
                _orders = value;

                OnPropertyChanged(nameof(Orders));
            }
        }
    }

    public class ItemsFilters
    {
        public string Name { get; set; }
        public string Category { get; set; }
    }
}
