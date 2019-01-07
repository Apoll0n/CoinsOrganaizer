using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using CoinsOrganizerDesktop.Database.BusinessLogic;

namespace CoinsOrganizerDesktop.Helpers.Converters
{
    public class GroupDataContextToBuyPriceSumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var group = (CollectionViewGroup)value;
            double sum = 0.0;

            if (group != null)
            {
                foreach (var groupItem in group.Items)
                {
                    var order = (OrderBL)groupItem;
                    sum += order.CoinBL.Cost;
                }
            }

            return "Купили за " + sum + " грн.";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
