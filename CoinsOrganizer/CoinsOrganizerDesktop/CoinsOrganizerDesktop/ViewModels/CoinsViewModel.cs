using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinsOrganizerDesktop.Database.BusinessLogic;
using CoinsOrganizerDesktop.Database.DatabaseModels;
using CoinsOrganizerDesktop.Helpers;

namespace CoinsOrganizerDesktop.ViewModels
{
    public class CoinsViewModel : BaseViewModel
    {
        private BusinessLogic _businessLogic;

        public CoinsViewModel()
        {
            _businessLogic = new BusinessLogic();
            var coins = _businessLogic.GetCoins();
            Coins = new ObservableCollection<Coin>(coins);
        }

        public ObservableCollection<Coin> Coins { get; set; }
    }
}
