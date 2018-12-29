using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3.Model;

namespace CoinsOrganizerDesktop.ViewModels.Model
{
    public enum Currency
    {
        USD,
        UAH,
        PLN
    }

    public static class CurrencyHelper
    {
        public static string ConvertToSign(Currency currency)
        {
            var result = string.Empty;

            if (currency == Currency.PLN)
            {
                result = "zł";
            }
            else if (currency==Currency.USD)
            {
                result = "$";
            }
            else
            {
                result = "грн";
            }

            return result;
        }
    }
}
