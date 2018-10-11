using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading;
using System.Threading.Tasks;
using AllegroWebService.WebApiService;
using AutoMapper;
using BusinessLogic.Entities;
using CoinsOrganizer.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoinsOrganizer.Controlers
{
    public class CoinsController : Controller
    {
        public static int currentEditionID;

        //public IActionResult Index()
        //{
        //    return ShowAllPage();
        //}

        public IActionResult AddPage()
        {
            return View();
        }
        public IActionResult AddCoin(string title, int cost, string link)
        {
            CoinModel coin = new CoinModel()
            {
                Name = title,
                Link = link,
                Cost = cost
            };

            using (var db = new BusinessLogic.BusinessLogic())
                db.AddCoin(Mapper.Map<CoinBL>(coin));

            return Redirect("~/Home/Index");
        }

        [HttpPost]
        [AllowAnonymous]
        public string AddMinorChanges(string changedText)
        {
            return changedText + " was submitted!";
        }
        public IActionResult AddMinorChanges2()
        {
            return new EmptyResult();
            //return Redirect("~/Coin/ShowAllPage");
        }

        public IActionResult SearchPage()
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
            // List the tree of categories available on the allegro site
            var categories = client.doGetCatsData(siteId, 0, webApiKey, true, out versionKey, out versionString);

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
            var items = client.doGetMySellItems(login,
                null, null, null, 9333, null, 100, 0, out itemStructs);
            //AllegroAPI.doGetItemsListRequest getlist = new AllegroAPI.doGetItemsListRequest();
            //getlist.webapiKey = "c7311ab6b61744c489171efa5d5367a4";
            //getlist.countryId = 1;

            //AllegroAPI.doGetItemsListResponse getlistresp = new AllegroAPI.doGetItemsListResponse();
            /*
        DeleteAllCoins();

        string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        string ApplicationName = "Google Sheets API .NET Quickstart";

        UserCredential credential;

        using (var stream =
            new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
        {
            string credPath = "token.json";
            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                Scopes,
                "user",
                CancellationToken.None,
                new FileDataStore(credPath, true)).Result;
            Console.WriteLine("Credential file saved to: " + credPath);
        }

        // Create Google Sheets API service.
        var service = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = ApplicationName,
        });

        // Define request parameters.
        String spreadsheetId = "13Hh_x_kU9BhBryuIwwGvB6r0PpztjZaVu8y5-5BbeTk";
        String range = "New!A2:N1178";
        SpreadsheetsResource.ValuesResource.GetRequest request =
                service.Spreadsheets.Values.Get(spreadsheetId, range);

        // Prints the names and majors of students in a sample spreadsheet:
        // https://docs.google.com/spreadsheets/d/1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms/edit
        ValueRange response = request.Execute();
        IList<IList<Object>> values = response.Values;
        if (values != null && values.Count > 0)
        {
            //Console.WriteLine("Name, Major");
            foreach (var row in values)
            {
                CoinModel coin = new CoinModel();

                if (row.Count == 14)
                {
                    var index = int.Parse(row[0].ToString());
                    var title = row[3].ToString();
                    double price = 0d;
                    if (row[4].ToString() != "")
                    {
                        price = double.Parse(row[4].ToString().Replace("грн.", "").Replace(",", ""));
                    }

                    var link = row[5].ToString();

                    double allegroPrice = 0d;
                    if (row[9].ToString() != "")
                    {
                        allegroPrice = double.Parse(row[9].ToString().Replace(" zł", "").Replace(",", ""));
                    }

                    var allegroName = row[10].ToString();
                    double dollarPrice = 0d;
                    if (row[11].ToString() != "")
                    {
                        dollarPrice = double.Parse(row[11].ToString().Replace("$", "").Replace(",", ""));
                    }
                    var englishName = row[12].ToString();

                    bool isInStock = row[7].ToString() != "#N/A";
                    var avers = row[1].ToString();
                    var revers = row[2].ToString();

                    if (avers == "foto")
                    {
                        avers = string.Empty;
                    }

                    if (revers == "foto")
                    {
                        revers = string.Empty;
                    }

                    coin = new CoinModel
                    {
                        CoinId = index,
                        Name = title,
                        Cost = price,
                        Link = link,
                        //OrderForeignKey = index,
                        ZlotyPrice = allegroPrice,
                        PolishName = allegroName,
                        DollarPrice = dollarPrice,
                        EnglishName = englishName,
                        IsInStock = isInStock,
                        AversFotoLink = avers,
                        ReversFotoLink = revers
                    };
                }
                else
                {

                }
                // Print columns A and E, which correspond to indices 0 and 4.
                //Console.WriteLine("{0}, {1}", row[0], row[4]);
                if (coin.Link == null)
                {

                }


                using (var db = new BusinessLogic.BusinessLogic())
                    db.AddCoin(Mapper.Map<CoinBL>(coin));
            }
        }
        */

            return View();
        }

        public IActionResult SearchResult(string title, string link)
        {
            List<CoinModel> coins = new List<CoinModel>();

            using (var db = new BusinessLogic.BusinessLogic())
            {
                foreach (CoinModel coin in Mapper.Map<List<CoinModel>>(db.GetCoins()))
                {
                    if (coin.Name == title && coin.Link == link)
                        coins.Add(coin);
                }
            }
            return View("ShowAllPage", coins);
        }

        public IActionResult ShowAllPage()
        {
            List<CoinModel> list = null;

            using (var db = new BusinessLogic.BusinessLogic())
                list = Mapper.Map<List<CoinModel>>(db.GetCoins());
            list.Reverse();
            return View(list);
        }
        public IActionResult Delete(int id)
        {
            using (var db = new BusinessLogic.BusinessLogic())
                db.RemoveCoin(id);

            return Redirect("~/Home/Index");
        }
        public IActionResult DeleteAllCoins()
        {
            using (var db = new BusinessLogic.BusinessLogic())
            {
                foreach (CoinModel coin in Mapper.Map<List<CoinModel>>(db.GetCoins()))
                    db.RemoveCoin(coin.CoinId);
            }

            return Redirect("~/Home/Index");
        }

        public IActionResult EditPage(int id)
        {
            CoinModel coin = null;

            using (var db = new BusinessLogic.BusinessLogic())
            {
                var players = Mapper.Map<List<CoinModel>>(db.GetCoins());
                foreach (CoinModel model in players)
                {
                    if (model.CoinId == id)
                    {
                        coin = model;
                        break;
                    }
                }
            }

            if (coin != null)
            {
                currentEditionID = coin.CoinId;
                ViewData["CoinName"] = coin.Name;
                ViewData["CoinCost"] = coin.Cost;
                ViewData["CoinLink"] = coin.Link;
                ViewData["CoinDollarPrice"] = coin.DollarPrice;
                ViewData["CoinZlotyPrice"] = coin.ZlotyPrice;
                ViewData["CoinIsSold"] = coin.IsSold;
                ViewData["CoinEnglishName"] = coin.EnglishName;
                ViewData["CoinPolishName"] = coin.PolishName;
            }

            return View();
        }
        public IActionResult SaveChanges(int id, string name, string surname, string year, bool health, int salary)
        {
            CoinModel coin = new CoinModel()
            {
                CoinId = currentEditionID,
                Name = name,
            };

            using (var db = new BusinessLogic.BusinessLogic())
                db.UpdateCoin(Mapper.Map<CoinBL>(coin));

            return Redirect("~/Home/Index");
        }
    }
}
