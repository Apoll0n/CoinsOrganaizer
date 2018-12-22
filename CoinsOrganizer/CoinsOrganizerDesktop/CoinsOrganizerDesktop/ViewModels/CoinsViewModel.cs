using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CoinsOrganizerDesktop.Database.BusinessLogic;
using CoinsOrganizerDesktop.Database.DatabaseModels;
using CoinsOrganizerDesktop.Helpers;
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
        private ObservableCollection<CoinBL> _coins;

        public CoinsViewModel()
        {
            _businessLogic = new BusinessLogic();
            var coinsbl = _businessLogic.GetCoinsBL().Where(x => x.IsInStock);
            Coins = new ObservableCollection<CoinBL>(coinsbl);

            NewCoin = new NewCoinModel();

            //var coinsInStock = coinsbl.Where(w => w.OrderBL == null).Select(x => x.CoinId);
            //var allegroCoins = AllegroService.ActiveList;

            //var hasInd = allegroCoins.Where(x => x.itemTitle.Contains("(") && x.itemTitle.Contains(")"));
            //var dontHaveInd = allegroCoins
            //    .Where(x => !x.itemTitle.Contains("(") && !x.itemTitle.Contains(")")) /*.Select(x => x.itemTitle)*/;
            //var indexes = hasInd
            //    //    .Where(x=>
            //    //{
            //    //    var ind = x.itemTitle.LastIndexOf("(", StringComparison.InvariantCulture);
            //    //    var coinIndex = x.itemTitle.Substring(ind).Replace(")", "").Replace("(", "");
            //    //    int result;
            //    //    return int.TryParse(coinIndex, out result);
            //    //})
            //    .SelectMany(x =>
            //        {
            //            var ind = x.itemTitle.LastIndexOf("(", StringComparison.InvariantCulture);
            //            var coinIndex = x.itemTitle.Substring(ind).Replace(")", "").Replace("(", "");
            //            int result;
            //            if (!int.TryParse(coinIndex, out result))
            //            {
            //                return new[] {new {Index = -1, ItemID = x.itemId, Index2 = coinIndex, x}};
            //            }
            //            return new[] {new {Index = result, ItemID = x.itemId, Index2 = coinIndex, x}};
            //        }

            //    ).OrderBy(x => x.Index);
            //var indexesList = indexes.ToList();

            //var duplicates = indexes
            //    .GroupBy(i => i.Index)
            //    .Where(g => g.Count() > 1);

            //var allegroCoinsInndexex = indexes.Skip(18).Select(x => x.Index);

            //var unicInd = new int[]{ 4061, 4062, 4063, 4064, 4065, 4066, 4067, 4068, 4069, 4070, 4071, 4072, 4073, 4074, 4075, 4076, 4077, 4078, 4079, 4080, 4081, 4082, 4083, 4084, 4085, 4086, 4087, 4088, 4089, 4090, 4091, 4092, 4093, 4051, 4052, 4053, 4054, 4055, 4056, 4057, 4058, 4059, 4060 }.Except(allegroCoinsInndexex).ToArray();

            //foreach (var indeax in allegroCoinsInndexex)
            //{
            //    if (unicInd.Any(x=>x==indeax))
            //    {

            //    }
            //}
            ////var adsasd = allegroCoinsInndexex

            //string[] mas = new string[unicInd.Count()];
            //string mas2 = String.Empty;
            //for (int i = 0; i < unicInd.Count(); i++)
            //{
            //    var index = unicInd[i];
            //    mas2 += index + ", ";
            //}

            //File.WriteAllText("indexes.txt", mas2);


            //var sdfsdf = dontHaveInd.Select(x => x.itemTitle);
        }

        public NewCoinModel NewCoin { get; set; }

        public CoinTableState TableState { get; set; }
        public bool SortByDescending { get; set; }

        public ICommand AddNewCoinCommand
        {
            get
            {
                return new ActionCommand(() =>
                {

                    var coin = new Coin
                    {
                        AversFotoLink = NewCoin.AvFotoLink,
                        ReversFotoLink = NewCoin.RevFotoLink,
                        Cost = double.Parse(NewCoin.Price),
                        Name = NewCoin.Name,
                        Link = NewCoin.Link,
                        CoinId = _businessLogic.GetCoinsBL().Max(x => x.CoinId) + 1
                    };

                    _businessLogic.AddCoin(coin);

                    _businessLogic.ApplyChanges();

                    OnPropertyChanged(nameof(Coins));
                    ChangeTableState(TableState);
                    SortTableIndex(SortByDescending);
                });
            }
        }

        public ICommand DeleteCoinCommand
        {
            get
            {
                return new ActionCommand<CoinBL>((e) =>
                {
                    _businessLogic.RemoveCoin(((CoinBL) e).CoinId);
                    _businessLogic.ApplyChanges();

                    ChangeTableState(TableState);
                    SortTableIndex(SortByDescending);
                });
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

        public ICommand SortCoinsCommand
        {
            get
            {
                return new ActionCommand<object>((e) =>
                {
                    SortByDescending = (bool) e;
                    SortTableIndex(SortByDescending);
                });
            }
        }

        public void SortTableIndex(bool byDescending)
        {

            if (byDescending)
            {
                Coins = new ObservableCollection<CoinBL>(Coins.OrderByDescending(x => x.CoinId));
            }
            else
            {
                Coins = new ObservableCollection<CoinBL>(Coins.OrderBy(x => x.CoinId));
            }
        }

        public void ChangeTableState(CoinTableState state)
        {
            IEnumerable<CoinBL> coins = Enumerable.Empty<CoinBL>();

            if (state == CoinTableState.All)
            {
                coins = _businessLogic.GetCoinsBL();
            }
            else if (state == CoinTableState.Available)
            {
                coins = _businessLogic.GetCoinsBL().Where(x => x.IsInStock);
            }
            else if (state == CoinTableState.Sold)
            {
                coins = _businessLogic.GetCoinsBL().Where(x => x.IsSold);
            }

            Coins = new ObservableCollection<CoinBL>(coins);
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

        public ObservableCollection<CoinBL> Coins
        {
            get { return _coins; }
            set
            {
                _coins = value;
                OnPropertyChanged(nameof(Coins));
            }
        }
    }
}
