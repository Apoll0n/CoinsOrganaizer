﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CoinsOrganizerDesktop.Database.DatabaseModels;
using CoinsOrganizerDesktop.DataBase.DbContext;
using CoinsOrganizerDesktop.MarketService;
using CoinsOrganizerDesktop.ViewModels;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;

namespace CoinsOrganizerDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Coin> _coinsList;
        private List<Order> _ordersList;
        public MainWindow()
        {
            InitializeComponent();
            _coinsList = new List<Coin>();
            DataContext = new MainWindowViewModel();
            _ordersList = new List<Order>();


            //AllegroService.EditItem();

            /*var connectionString = ConfigurationManager.AppSettings["DbConnectionString"];
            using (var db = new CoinsOrganizerContext(connectionString))
            {
                var orders = db.Orders;
                //var coins = db.Coins;

                foreach (var order1 in orders)
                {
                    //var order = order1.Coin;
                    db.Orders.Remove(order1);

                }

                //foreach (var coin1 in coins)
                //{
                //    //var coin = coin1.Order;
                //    //coin.OrderId = Orders
                //    db.Coins.Remove(coin1);

                //    //var orderCollection = orders.Where(x => x.CoinId == coin1.CoinId).Select(x=>x.Name);
                //    //if (orderCollection != null && orderCollection.Count()>0)
                //    //{

                //    //}
                //}
                db.SaveChanges();
            }

            UploadOrders();*/




            /*string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
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
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Drive Oauth2 Authentication Sample"
            });


            string pageToken = null;
            do
            {
                var request = service.Files.List();
                request.Q = "mimeType='image/png'";
                request.Spaces = "drive";
                request.Fields = "nextPageToken, files(id, name)";
                request.PageToken = pageToken;
                var result = request.Execute();
                foreach (var file in result.Files)
                {
                    Console.WriteLine(String.Format(
                        "Found file: {0} ({1})", file.Name, file.Id));
                }
                pageToken = result.NextPageToken;
            } while (pageToken != null);*/


            //using (var db = new CoinsOrganizerContext(connectionString))
            //{
            //    var coins = db.Coins.OrderBy(x => x.Name);

            //    foreach (var coin in coins)
            //    {
            //        coin.IsSold = true;
            //        //var order = order1.Coin;
            //        //db.Orders.Remove(order1);

            //    }
            //    db.SaveChanges();
            //}
            //var connectionString = ConfigurationManager.AppSettings["DbConnectionString"];
            //var db = new CoinsOrganizerContext(connectionString);
            /* using (var db = new CoinsOrganizerContext(connectionString))
             {

                 //var coin = new Coin() { CoinId = 2, Cost = 11, Name = "Poltorak1", Link = "link" };
                 //var coin2 = new Coin() { CoinId = 3, Cost = 12, Name = "Poltorak2", Link = "link" };
                 //db.Coins.Add(coin);
                 //db.Coins.Add(coin2);
                 //db.SaveChanges();

                 ////var query = db.Coins.OrderBy(x => x.Name);

                 ////foreach (var coin1 in query)
                 ////{

                 ////}


                 //var order = new Order()
                 //{
                 //    CoinId = 2,
                 //    Name = "Polt",
                 //    NickName = "apollon",
                 //    SalePrice = 10,
                 //    WhereSold = "Allegro"
                 //};
                 //var order2 = new Order()
                 //{
                 //    CoinId = 3,
                 //    Name = "Polt2",
                 //    NickName = "apollon2",
                 //    SalePrice = 10,
                 //    WhereSold = "Allegro2"
                 //};
                 //db.Orders.Add(order);
                 //db.Orders.Add(order2);
                 //db.SaveChanges();

                 var orders = db.Orders.OrderBy(x => x.Name);
                 var coins = db.Coins.OrderBy(x => x.Name);

                 foreach (var order1 in orders)
                 {
                     //var order = order1.Coin;
                     //db.Orders.Remove(order1);

                 }
                 db.SaveChanges();

                 foreach (var coin1 in coins)
                 {
                     //var coin = coin1.Order;
                     //coin.OrderId = Orders
                     //db.Coins.Remove(coin1);

                     //var orderCollection = orders.Where(x => x.CoinId == coin1.CoinId).Select(x=>x.Name);
                     //if (orderCollection != null && orderCollection.Count()>0)
                     //{

                     //}
                 }
                 db.SaveChanges();
             }

             //CheckAllegroIndexes();

             UploadCoins();
             UploadOrders();*/
        }

        private void CheckAllegroIndexes()
        {
            var hasInd = AllegroService.ActiveList.Where(x => x.itemTitle.Contains("(") && x.itemTitle.Contains(")"));
            var dontHaveInd = AllegroService.ActiveList
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

        private void UploadOrders()
        {
            string[] Scopes = {SheetsService.Scope.SpreadsheetsReadonly};
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
            String range = "'Висилка'!A2:I1411";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                service.Spreadsheets.Values.Get(spreadsheetId, range);

            // Prints the names and majors of students in a sample spreadsheet:
            // https://docs.google.com/spreadsheets/d/1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms/edit
            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;
            if (values != null && values.Count > 0)
            {

                var connectionString = ConfigurationManager.AppSettings["DbConnectionString"];
                using (var db = new CoinsOrganizerContext(connectionString))
                {
                    foreach (var row in values)
                    {
                        Order order = null;

                        if (row.Count == 9)
                        {
                            var index = int.Parse(row[8].ToString() == "." ? "-1" : row[8].ToString());
                            var title = row[0].ToString();
                            var paid = row[1].ToString() == "т";
                            double price = 0d;

                            var isDollar = row[4].ToString().Contains("$");
                            var isZloty = row[4].ToString().Contains("zł");
                            var isUah = row[4].ToString().Contains("грн");
                            //if (row[4].ToString() != "")
                            //{
                            //    price = double.Parse(row[4].ToString().Replace("$.", "").Replace(",", "").Replace(" zł", ""));
                            //}

                            var email = row[3].ToString();
                            var nickname = row[2].ToString();

                            double soldprice = -1d;
                            if (row[4].ToString() != "")
                            {
                                soldprice = double.Parse(row[4].ToString().Replace(" zł", "").Replace(" ", "")
                                    .Replace("грн", "").Replace(",", "")
                                    .Replace("$", ""));
                            }

                            var trackNumber = row[5].ToString();
                            var orderdetails = row[6].ToString() + " " + row[7].ToString();
                            //double dollarPrice = 0d;
                            //if (row[11].ToString() != "")
                            //{
                            //    dollarPrice = double.Parse(row[11].ToString().Replace("$", "").Replace(",", ""));
                            //}
                            //var englishName = row[12].ToString();

                            //bool isInStock = row[7].ToString() != "#N/A";
                            //var avers = row[1].ToString();
                            //var revers = row[2].ToString();

                            //if (avers == "foto")
                            //{
                            //    avers = string.Empty;
                            //}

                            //if (revers == "foto")
                            //{
                            //    revers = string.Empty;
                            //}

                            order = new Order()
                            {
                                NickName = nickname.Equals("") ? "." : nickname,
                                IsPaid = paid,
                                TrackNumber = trackNumber,
                                OrderDetails = orderdetails,
                                Email = email.Equals("") ? "." : email,
                                CoinId = index,
                                Name = title,
                                SalePrice = soldprice,
                                SaleCurrency = isZloty ? "zł" : isUah ? "UAH" : "$",
                                WhereSold = isZloty ? "Allegro" : isUah ? "в руки" : "Ebay",
                                Link = string.Empty
                                //CoinId = index,
                                //Name = title,
                                //Cost = price,
                                //Link = link,
                                //ZlotyPrice = allegroPrice,
                                //PolishName = allegroName,
                                //DollarPrice = dollarPrice,
                                //EnglishName = englishName,
                                //IsInStock = isInStock,
                                //AversFotoLink = avers,
                                //ReversFotoLink = revers
                            };
                        }
                        else
                        {

                        }
                        // Print columns A and E, which correspond to indices 0 and 4.
                        //Console.WriteLine("{0}, {1}", row[0], row[4]);
                        if (order == null || order.Name == "" || order.NickName == "")
                        {

                        }
                        if (order != null && db.Coins.Any(x => x.CoinId.Equals(order.CoinId)) && order.Name != null &&
                            !order.Name.Equals(""))
                        {
                            _ordersList.Add(order);
                            db.Orders.Add(order);
                            //using (var db = new BusinessLogic.BusinessLogic())
                            //    db.AddOrder(Mapper.Map<OrderBL>(order));
                        }
                    }

                    db.SaveChanges();
                }
            }
        }

        private void UploadCoins()
        {
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
            //String range = "New!A1351:N1355";
            String range = "New!A2:N2144";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadsheetId, range);

            // Prints the names and majors of students in a sample spreadsheet:
            // https://docs.google.com/spreadsheets/d/1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms/edit
            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;
            if (values != null && values.Count > 0)
            {
                var connectionString = ConfigurationManager.AppSettings["DbConnectionString"];
                using (var db = new CoinsOrganizerContext(connectionString))
                {
                    //Console.WriteLine("Name, Major");
                    foreach (var row in values)
                    {
                        Coin coin = null;

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

                            coin = new Coin
                            {
                                CoinId = index,
                                Name = title,
                                Cost = price,
                                Link = link,
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

                        if (coin != null && coin.Link == null)
                        {
                        }
                        if (coin != null /*&& db.Orders.Any(x => x.CoinId.Equals(coin.CoinId))*/)
                        {
                            _coinsList.Add(coin);
                            db.Coins.Add(coin);
                            //db.SaveChanges();
                        }


                    }

                    db.SaveChanges();
                }
            }
        }
    }

    //public class DriveListExample
    //{
    //    public class FilesListOptionalParms
    //    {
    //        /// 

    //        /// The source of files to list.
    //        /// 
    //        public string Corpus { get; set; }
    //        /// 

    //        /// A comma-separated list of sort keys. Valid keys are 'createdTime', 'folder', 'modifiedByMeTime', 'modifiedTime', 'name', 'quotaBytesUsed', 'recency', 'sharedWithMeTime', 'starred', and 'viewedByMeTime'. Each key sorts ascending by default, but may be reversed with the 'desc' modifier. Example usage: ?orderBy=folder,modifiedTime desc,name. Please note that there is a current limitation for users with approximately one million files in which the requested sort order is ignored.
    //        /// 
    //        public string OrderBy { get; set; }
    //        /// 

    //        /// The maximum number of files to return per page.
    //        /// 
    //        public int? PageSize { get; set; }
    //        /// 

    //        /// The token for continuing a previous list request on the next page. This should be set to the value of 'nextPageToken' from the previous response.
    //        /// 
    //        public string PageToken { get; set; }
    //        /// 

    //        /// A query for filtering the file results. See the "Search for Files" guide for supported syntax.
    //        /// 
    //        public string Q { get; set; }
    //        /// 

    //        /// A comma-separated list of spaces to query within the corpus. Supported values are 'drive', 'appDataFolder' and 'photos'.
    //        /// 
    //        public string Spaces { get; set; }
    //        /// 

    //        /// Selector specifying a subset of fields to include in the response.
    //        /// 
    //        public string fields { get; set; }
    //        /// 

    //        /// Alternative to userIp.
    //        /// 
    //        public string quotaUser { get; set; }
    //        /// 

    //        /// IP address of the end user for whom the API call is being made.
    //        /// 
    //        public string userIp { get; set; }
    //    }

    //    /// 

    //    /// Lists or searches files. 
    //    /// Documentation https://developers.google.com/drive/v3/reference/files/list
    //    /// Generation Note: This does not always build corectly.  Google needs to standardise things I need to figuer out which ones are wrong.
    //    /// 
    //    /// Authenticated drive service.  
    //    /// Optional paramaters.        /// FileListResponse
    //    public static Google.Apis.Drive.v3.Data.FileList ListFiles(DriveService service, FilesListOptionalParms optional = null)
    //    {
    //        try
    //        {
    //            // Initial validation.
    //            if (service == null)
    //                throw new ArgumentNullException("service");

    //            // Building the initial request.
    //            var request = service.Files.List();
    //            // Applying optional parameters to the request.                
    //            request = (FilesResource.ListRequest)SampleHelpers.ApplyOptionalParms(request, optional);
    //            // Requesting data.
    //            return request.Execute();
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception("Request Files.List failed.", ex);
    //        }
    //    }


    //}
    //public static class SampleHelpers
    //{

    //    /// 

    //    /// Using reflection to apply optional parameters to the request.  
    //    /// 
    //    /// If the optonal parameters are null then we will just return the request as is.
    //    /// 
    //    /// The request. 
    //    /// The optional parameters. 
    //    /// 
    //    public static object ApplyOptionalParms(object request, object optional)
    //    {
    //        if (optional == null)
    //            return request;

    //        System.Reflection.PropertyInfo[] optionalProperties = (optional.GetType()).GetProperties();

    //        foreach (System.Reflection.PropertyInfo property in optionalProperties)
    //        {
    //            // Copy value from optional parms to the request.  They should have the same names and datatypes.
    //            System.Reflection.PropertyInfo piShared = (request.GetType()).GetProperty(property.Name);
    //            if (property.GetValue(optional, null) != null) // TODO Test that we do not add values for items that are null
    //                piShared.SetValue(request, property.GetValue(optional, null), null);
    //        }

    //        return request;
    //    }
    //}
}
