using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CoinsOrganizerDesktop.Database.BusinessLogic;
using CoinsOrganizerDesktop.Database.DatabaseModels;
using CoinsOrganizerDesktop.Helpers;

namespace CoinsOrganizerDesktop.ViewModels
{
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
        }

        public ICommand ShowOnlyAvailableCoinsCommand
        {
            get
            {
                return new ActionCommand(() =>
                {
                    var coinsbl = _businessLogic.GetCoinsBL().Where(x => x.IsInStock);

                    Coins = new ObservableCollection<CoinBL>(coinsbl);

                });
            }
        }

        public ICommand ShowAllCoinsCommand
        {
            get
            {
                return new ActionCommand(() =>
                {
                    var coinsbl = _businessLogic.GetCoinsBL();

                    Coins = new ObservableCollection<CoinBL>(coinsbl);

                });
            }
        }

        public ICommand ShowSoldCoinsCommand
        {
            get
            {
                return new ActionCommand(() =>
                {
                    var coinsbl = _businessLogic.GetCoinsBL().Where(x => x.IsSold);

                    Coins = new ObservableCollection<CoinBL>(coinsbl);

                });
            }
        }

        public ICommand SortCoinsCommand
        {
            get
            {
                return new ActionCommand<object>((e) =>
                {
                    if ((bool) e)
                    {
                        Coins = new ObservableCollection<CoinBL>(Coins.OrderByDescending(x => x.CoinId));
                    }
                    else
                    {
                        Coins = new ObservableCollection<CoinBL>(Coins.OrderBy(x => x.CoinId));
                    }

                });
            }
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
