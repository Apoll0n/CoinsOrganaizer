using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using CoinsOrganizerDesktop.Database.BusinessLogic;
using CoinsOrganizerDesktop.Database.DatabaseModels;
using CoinsOrganizerDesktop.Helpers;
using CoinsOrganizerDesktop.Helpers.Converters;
using CoinsOrganizerDesktop.MarketService;
using CoinsOrganizerDesktop.ViewModels.Model;

namespace CoinsOrganizerDesktop.ViewModels
{
    public enum CoinTableState
    {
        All,
        Available,
        Sold
    }


    public class CoinsViewModel : BaseViewModel
    {
        private BusinessLogic _businessLogic;
        private CoinBL _selectedRow;
        private ObservableCollection<Coin> _coins;

        public CoinsViewModel()
        {
            _businessLogic = new BusinessLogic();
            var coinsLocal = _businessLogic.GetCoinsLocal();

            var maxInd = coinsLocal.Max(x => x.CoinId) + 1;
            Coins = coinsLocal;

            NewCoin = new NewCoinModel();

            List<ItemsFilters> items = new List<ItemsFilters>();
            items.Add(new ItemsFilters() { Name = "Всі", Category = "A" });
            items.Add(new ItemsFilters() { Name = "Наявні", Category = "B" });
            items.Add(new ItemsFilters() { Name = "Продані", Category = "B" });
            items.Add(new ItemsFilters() { Name = "Вист. на Allegro", Category = "C" });
            items.Add(new ItemsFilters() { Name = "Вист. на Allegro x2", Category = "C" });
            items.Add(new ItemsFilters() { Name = "Не вист. на Allegro", Category = "C" });
            items.Add(new ItemsFilters() { Name = "Oferta на Allegro", Category = "C" });
            items.Add(new ItemsFilters() { Name = "Вист. на Allegro, не в наявності", Category = "C" });

            ICollectionView groupedCoinsFilters = new ListCollectionView(items);
            groupedCoinsFilters.GroupDescriptions.Add(new PropertyGroupDescription("Category"));
            CoinsFiltersSource = groupedCoinsFilters;

            //var collection = new ListCollectionView(coinsLocal);
            //Coins = collection;

            SelectedItemFilter = items[0];
            //collection.Filter = CoinFilter;
        }

        private ItemsFilters _selectedItemFilter;

        public ItemsFilters SelectedItemFilter
        {
            get { return _selectedItemFilter; }
            set
            {
                _selectedItemFilter = value;
                //Coins.Refresh();
            }
        }

        public ICollectionView CoinsFiltersSource { get; set; }

        public NewCoinModel NewCoin { get; set; }

        public CoinTableState TableState { get; set; }
        public bool SortByDescending { get; set; }

        public ICommand AddNewCoinCommand
        {
            get
            {
                return new ActionCommand(() =>
                {
                    //Task.Factory.StartNew(() =>      
                    //{
                        var coin = new Coin
                        {
                            AversFotoLink = NewCoin.AvFotoLink,
                            ReversFotoLink = NewCoin.RevFotoLink,
                            Cost = double.Parse(NewCoin.Price),
                            Name = NewCoin.Name,
                            IsInStock = true,
                            Link = NewCoin.Link,
                            CoinId = _businessLogic.GetCoinsLocal().Max(x => x.CoinId) + 1
                        };

                        _businessLogic.AddCoin(coin);
                        _businessLogic.ApplyChanges();

                        //var coinsbl = _businessLogic.GetCoinsBL();
                        //var newItem = coinsbl.Single(x => x.CoinId.Equals(coin.CoinId));
                        //return newItem;

                    //}).ContinueWith((r) =>
                    //{
                        //Coins.AddNewItem(r.Result);
                        //Coins = new ListCollectionView((IList) r.Result);

                        OnPropertyChanged(nameof(Coins));
                        //ChangeTableState(TableState);
                        //SortTableIndex(SortByDescending);
                //    }, TaskScheduler.FromCurrentSynchronizationContext());
                });
            }
        }

        public ICommand DeleteCoinCommand
        {
            get
            {
                return new ActionCommand<CoinBL>((e) =>
                {
                    var coin = (CoinBL) e;
                    MessageBoxResult result = MessageBox.Show(string.Format(" Видалити?\n{1} ({0})", coin.CoinId, coin.Name),
                        "Confirmation", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        _businessLogic.RemoveCoin(coin.CoinId);
                        _businessLogic.ApplyChanges();

                        ChangeTableState(TableState);
                        SortTableIndex(SortByDescending);
                    }
                });
            }
        }

        public void SortTableIndex(bool byDescending)
        {
            //Coins.SortDescriptions.Add(new SortDescription("CoinId",
            //    byDescending ? ListSortDirection.Descending : ListSortDirection.Ascending));
        }

        public void ChangeTableState(CoinTableState state)
        {
            //Coins.Filter = CoinFilter;
            //Coins.Refresh();
            //OnPropertyChanged(nameof(Coins));
        }

        private bool CoinFilter(object obj)
        {
            bool result = true;

            var coin = new CoinBL((Coin) obj);


            switch (SelectedItemFilter.Name)
            {
                case "Всі":

                    result = true;

                    break;
                case "Наявні":

                    result = coin.IsInStock;

                    break;
                case "Продані":

                    result = coin.IsSold;

                    break;
                case "Вист. на Allegro":

                    result = coin.IsOnAllegroSale;

                    break;
                case "Вист. на Allegro x2":

                    result = coin.HasSuperfluousItemOnAllegroSale;

                    break;
                case "Не вист. на Allegro":

                    result = !coin.IsOnAllegroSale && coin.IsInStock;

                    break;
                case "Вист. на Allegro, не в наявності":

                    result = coin.IsOnAllegroSale && !coin.IsInStock;

                    break;
                case "Oferta на Allegro":

                    result = coin.AllegroItemBiddersCount > 0;

                    break;
                default: break;
            }

            //if (TableState == CoinTableState.Available)
            //{
            //    result = coin.IsInStock;
            //}
            //else if (TableState == CoinTableState.Sold)
            //{
            //    result = coin.IsSold;
            //}

            return result;
        }

        public CoinBL SelectedRow
        {
            get
            {
                return _selectedRow;
            }
            set
            {
                _businessLogic.UpdateCoin(_selectedRow);
                _selectedRow = value;
            }
        }

        public ObservableCollection<Coin> Coins
        {
            get { return _coins; }
            set
            {
                _coins = value;
                OnPropertyChanged(nameof(Coins));
            }
        }

        public ICommand ShowOnlyAvailableCoinsCommand
        {
            get
            {
                return new ActionCommand(() =>
                {
                    TableState = CoinTableState.Available;
                    ChangeTableState(TableState);
                    SortTableIndex(SortByDescending);
                });
            }
        }

        public ICommand ShowAllCoinsCommand
        {
            get
            {
                return new ActionCommand(() =>
                {
                    TableState = CoinTableState.All;
                    ChangeTableState(TableState);
                    SortTableIndex(SortByDescending);
                });
            }
        }

        public ICommand ShowSoldCoinsCommand
        {
            get
            {
                return new ActionCommand(() =>
                {
                    TableState = CoinTableState.Sold;
                    ChangeTableState(TableState);
                    SortTableIndex(SortByDescending);
                });
            }
        }

        public ICommand GroupCoinsCommand
        {
            get
            {
                return new ActionCommand<object>((e) =>
                {
                    if ((bool)e)
                    {
                        //Coins.GroupDescriptions.Add(new PropertyGroupDescription("Link"));
                    }
                    else
                    {
                        //Coins.GroupDescriptions.Clear();
                    }
                });
            }
        }

        public ICommand SortCoinsCommand
        {
            get
            {
                return new ActionCommand<object>((e) =>
                {
                    SortByDescending = (bool)e;
                    SortTableIndex(SortByDescending);
                });
            }
        }

    }
}
