using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using CoinsOrganizerDesktop.AllegroWebApiService;
using eBay.Service.Call;
using eBay.Service.Core.Sdk;
using eBay.Service.Core.Soap;

namespace CoinsOrganizerDesktop.MarketService
{
    public static class AllegroService
    {
        private static servicePortClient _apiContext = null;
        private static string _login = null;
        private static DispatcherTimer _timer = null;

        public static List<SellItemStruct> ActiveList { get; set; }
        public static List<SoldItemStruct> SoldList { get; set; }

        public static void InitializeAllegro()
        {
            GetApiContext();

            if (ActiveList == null)
            {
                ActiveList = new List<SellItemStruct>();
            }

            if (SoldList == null)
            {
                SoldList = new List<SoldItemStruct>();
            }

            if (_timer == null)
            {
                _timer = new DispatcherTimer(TimeSpan.FromMinutes(1), DispatcherPriority.Background,
                    RefreshAllegroData, Dispatcher.CurrentDispatcher);
                //_timer.Start();
                //_timer.
                RefreshAllegroData(null, null);
            }
        }

        public static void EditItem()
        {
            var editCopyItem = ActiveList.Single(x => x.itemId == 7658160599);
            AfterSalesServiceConditionsStruct afterSalesServiceConditionsStruct;
            string additionalServicesGroup; 
             var copyItemFields = _apiContext.doGetItemFields(_login, editCopyItem.itemId, out afterSalesServiceConditionsStruct, out additionalServicesGroup);
            //var copyItemFields2 =
            //    _apiContext.doSellSomeAgain()

            FieldsValue fsValu = copyItemFields.SingleOrDefault(x => x.fid == 6);
            //foreach (var copyItemField in copyItemFields)
            //{
            //    if (copyItemField.fvalueString!= "")
            //    {

            //    }
            //}
            fsValu.fvalueFloat = 14.99f;
            var editTargetItem = ActiveList.Single(x => x.itemId == 7653601016);

            var firstEdit = _apiContext.doChangeItemFields(_login, 7653601016,
                new FieldsValue[]
                {
                    fsValu
                    //new FieldsValue{fid = 6, fvalueFloat = 14.99f},
                    //new FieldsValue{fid = 29, fvalueInt = 0},

                }
                , null, 0, null, null, null, null);



        }


        public static void CheckDublicates()
        {
            var hasInd = ActiveList.Where(x => x.itemTitle.Contains("(") && x.itemTitle.Contains(")"));
            var dontHaveInd = ActiveList
                .Where(x => !x.itemTitle.Contains("(") && !x.itemTitle.Contains(")")).Select(x => x.itemTitle);
            var indexes = hasInd
                //    .Where(x=>
                //{
                //    var ind = x.itemTitle.LastIndexOf("(", StringComparison.InvariantCulture);
                //    var coinIndex = x.itemTitle.Substring(ind).Replace(")", "").Replace("(", "");
                //    int result;
                //    return int.TryParse(coinIndex, out result);
                //})
                .SelectMany(x =>
                    {
                        var ind = x.itemTitle.LastIndexOf("(", StringComparison.InvariantCulture);
                        var coinIndex = x.itemTitle.Substring(ind).Replace(")", "").Replace("(", "");
                        int result;
                        if (!int.TryParse(coinIndex, out result))
                        {
                            return new[] { new { Index = -1, Index2 = coinIndex, x } };
                        }
                        return new[] { new { Index = result, Index2 = coinIndex, x } };
                    }

                ).OrderBy(x => x.Index);

            var duplicates = indexes
                .GroupBy(i => i.Index)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key);
        }




        private static void RefreshAllegroData(object sender, EventArgs eventArgs)
        {
            ActiveList.Clear();
            SellItemStruct[] sellItems;
            SoldItemStruct[] soldItems;

            var itemsCount = _apiContext.doGetMySellItems(_login, null, null, null, 0, null, 0, 0, out sellItems);
            for (int i = 1; i < Math.Ceiling(itemsCount / 100d) + 1; i++)
            {
                foreach (var sellItem in sellItems)
                {
                    ActiveList.Add(sellItem);
                }

                _apiContext.doGetMySellItems(_login, null, null, null, 0, null, 0, i, out sellItems);
            }

            var itemsCount2 = _apiContext.doGetMySoldItems(_login, null, null, null, 0, null, 0, 0, out soldItems);
            for (int i = 1; i < Math.Ceiling(itemsCount2 / 100d) + 1; i++)
            {
                foreach (var soldItem in soldItems)
                {
                    SoldList.Add(soldItem);
                }

                _apiContext.doGetMySoldItems(_login, null, null, null, 0, null, 0, i, out soldItems);
            }

            
        }

        private static void GetApiContext()
        {
            if (_apiContext == null)
            {
                var webApiKey = "640314ba49b245f7be0a4fce0b76a6e0";

                var siteId = 1;
                var siteVersion = 1416396021;

                var binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport)
                {
                    MaxReceivedMessageSize = 15000000
                };

                _apiContext =
                    new servicePortClient(binding, new EndpointAddress("https://webapi.allegro.pl/service.php"));
                long userId, serverTime;
                var syst = _apiContext.doQueryAllSysStatus(1, webApiKey);
                _login = _apiContext.doLogin("mosze888", "KGZkRsY5ottgDuW", 1, webApiKey, syst[0].verKey, out userId,
                    out serverTime);
            }
        }
    }
}