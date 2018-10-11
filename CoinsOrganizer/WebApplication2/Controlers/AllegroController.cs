using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using AllegroWebService.WebApiService;
using Microsoft.AspNetCore.Mvc;

namespace CoinsOrganizer.Controlers
{
    public class AllegroController : Controller
    {
        public IActionResult Index()
        {
            return View("AllegroPage");
        }

        public IActionResult TestAllegroApi()
        {
            var webApiKey = "640314ba49b245f7be0a4fce0b76a6e0";

            // Dependending which allegro site you want to connect to you'll have to use different values
            var siteId = 1;
            var siteVersion = 1416396021;

            var binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport)
            {
                MaxReceivedMessageSize = 15000000 // You might have to adjust this depending on how much data the API calls return
            };

            var client = new servicePortClient(binding, new EndpointAddress("https://webapi.allegro.pl/service.php"));
            long versionKey = 1;
            string versionString = "";
            int parOut1;
            ItemsListType[] parOut2;
            CategoriesListType parOut3;
            FiltersListType[] parOut4;
            string[] parOut5;
            SellItemStruct[] itemStructs;
            SellItemStruct[] itemStructs2;
            // List the tree of categories available on the allegro site
            var categories = client.doGetCatsData(siteId, 0, webApiKey, true, out versionKey, out versionString);
            _categories = categories;
            //var items = client.doGetItemsList(webApiKey, siteId, new FilterOptionsType[0], new SortOptionsType(),
            //    500, 0, 500, out parOut1, out parOut2,
            //    out parOut3, out parOut4, out parOut5);
            long userId, serverTime;
            var syst = client.doQueryAllSysStatus(1, webApiKey);
            var numCat = categories.Where(x => x.catName.Equals("1587 - 1668"));
            //var countryId = client.doGetCountries(1, webApiKey);
            //var ukraine = countryId.Where(x => x.countryName.Equals("Ukraina"));
            var login = client.doLogin("mosze888", "KGZkRsY5ottgDuW", 1, webApiKey, syst[0].verKey, out userId,
                out serverTime);
            //var items = client.doGetMySellItems(login,
            //    null, null, null, 9333, null, 100, 0, out itemStructs);
            //var numCat2 = categories.Where(x => x.catName.Equals("Kolekcje"));
            //var numCat3 = categories.Where(x => x.catId.Equals(79201));
            var itemsCount = client.doGetMySellItems(login, null, null, null, 0, null, 0, 0, out itemStructs);
            for (int i = 1; i < Math.Ceiling(itemsCount/100d)+1; i++)
            {
                foreach (var sellItem in itemStructs)
                {
                    _cats.Add(sellItem);
                }
                client.doGetMySellItems(login, null, null, null, 0, null, 0, i, out itemStructs);

            }

            //var adsasd = categories.Where(x => x.catId.Equals(91098));

            //GetCategories(91098);

            //foreach (var catIndex in _catsIndexes)
            //{
            //    client.doGetMySellItems(login, null, null, null, 9333, null, 100, 0, out itemStructs);
            //    foreach (var sellItem in itemStructs)
            //    {
            //        _cats.Add(sellItem);
            //    }
            //}

            var hasInd = _cats.Where(x => x.itemTitle.Contains("(") && x.itemTitle.Contains(")"));
            var dontHaveInd = _cats.Where(x => !x.itemTitle.Contains("(") && !x.itemTitle.Contains(")"));
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
                            return new[] {new {Index = -1, Index2 = coinIndex, x}};
                        }
                        return new[] {new { Index = result, Index2 = coinIndex, x}};
                    }

                ).OrderBy(x=>x.Index);

            var duplicates = indexes
                .GroupBy(i => i.Index)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key);
            BidItemStruct[] bidItems;

            var adfadasd = client.doGetMyBidItems(login, null, null, 0, null, 0, 0, out bidItems);
            //var adfadasd2 = client.doGetBidItem2(login, null);

            
            return View("AllegroPage");
        }

        private CatInfoType[] _categories;
        private List<int> _catsIndexes = new List<int>();
        private List<SellItemStruct> _cats = new List<SellItemStruct>();

        private void GetCategories(int index)
        {
            _catsIndexes.Add(index);
            var cats = _categories.Where(x => x.catParent.Equals(index));

            foreach (var cat in cats)
            {
                GetCategories(cat.catId);
            }
        }
    }

    public class Indexes
    {
        public int Index { get; set; }
        public string Index2 { get; set; }

        public Indexes(int index, string index2)
        {
            Index = index;
            Index2 = index2;
        }
    }
    
}
