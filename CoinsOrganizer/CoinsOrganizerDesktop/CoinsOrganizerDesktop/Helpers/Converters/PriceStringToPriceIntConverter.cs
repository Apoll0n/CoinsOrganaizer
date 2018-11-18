using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CoinsOrganizerDesktop.Helpers.Converters
{
    public class PriceStringToPriceIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = Regex.Replace(value.ToString(), @"[a-zA-Z\sł$]+", string.Empty).Replace(",",".");

            double parseNumber;
            if (!double.TryParse(result, out parseNumber))
            {
                return -1d;
            }
            
            return parseNumber;
        }
    }
}
