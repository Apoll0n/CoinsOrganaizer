using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;

namespace DatabaseUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            //RemoveAllCoinData();
            //RemoveAllOrderData();

            //UploadCoins();
            UploadOrders();

            //UploadCoinsTest();
        }

        private static void RemoveAllCoinData()
        {
            var connectionString = ConfigurationManager.AppSettings["DbConnectionString"];

            using (var db = new CoinsOrganizerContext(connectionString))
            {
                var coins = db.Coins.ToList();

                foreach (var coin in coins)
                {
                    db.Coins.Remove(coin);
                }

                db.SaveChanges();
            }
        }

        private static void RemoveAllOrderData()
        {
            var connectionString = ConfigurationManager.AppSettings["DbConnectionString"];

            using (var db = new CoinsOrganizerContext(connectionString))
            {
                var orders = db.Orders.ToList();

                foreach (var order in orders)
                {
                    db.Orders.Remove(order);
                }

                db.SaveChanges();
            }
        }
        private static void UploadCoinsTest()
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
            String range = "Sheet28!D2:N3285";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadsheetId, range);

            // Prints the names and majors of students in a sample spreadsheet:
            // https://docs.google.com/spreadsheets/d/1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms/edit
            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;
            if (values != null && values.Count > 0)
            {
            }
        }

        private static void UploadCoins()
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
            String range = "New!A2:N4623";
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
                            if (title == "")
                            {
                                title = "?";
                            }

                            double price = 0d;
                            if (row[4].ToString() != "")
                            {
                                price = double.Parse(row[4].ToString().Replace("грн.", "").Replace(",", "").Replace(".", ","));
                            }

                            var link = row[5].ToString();
                            if (link == "")
                            {
                                link = "?";
                            }

                            double allegroPrice = 0d;
                            if (row[9].ToString() != "" && row[9].ToString() != "прод" && row[9].ToString() != "кал")
                            {
                                var stringRow = row[9].ToString();
                                var repl1 = stringRow.Replace(" zł", "");
                                var repl2 = repl1.Replace(",", "");
                                var repl3 = repl2.Replace(".", ",");
                                allegroPrice = double.Parse(repl3);
                            }

                            var allegroName = row[10].ToString();
                            double dollarPrice = 0d;
                            if (row[11].ToString() != "")
                            {
                                dollarPrice = double.Parse(row[11].ToString().Replace("$", "").Replace(".", ","));
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
                                ReversFotoLink = revers,
                                IsSold = isInStock
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
                            //_coinsList.Add(coin);
                            db.Coins.Add(coin);
                            //db.SaveChanges();
                        }


                    }

                    db.SaveChanges();
                }
            }
        }

        private static void UploadOrders()
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
            String range = "'Висилка'!A2:I3616";

            //SpreadsheetsResource.ValuesResource.AppendRequest request2 = service.Spreadsheets.Values.Append(new ValueRange(){}, spreadsheetId, "'Sheet27'!A2:I1714")
            //service.Spreadsheets.Values.Get(spreadsheetId, range);

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
                                    .Replace("грн.", "").Replace(",", "")
                                    .Replace("$", "").Replace(".", ","));
                            }

                            var trackNumber = row[5].ToString();
                            var orderdetails =
                                nickname + System.Environment.NewLine + email + row[6].ToString() == String.Empty
                                    ? ""
                                    : System.Environment.NewLine + row[6].ToString() +
                                      row[7].ToString() == String.Empty
                                        ? ""
                                        : System.Environment.NewLine + row[7].ToString();
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
                                WhereSold = isZloty ? "Allegro" : isUah ? "Інше" : "Ebay",
                                Link = string.Empty,
                                IsShipped = paid,
                                IsTrackedOnMarket = true
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

                        if (order.CoinId == -1)
                        {

                        }

                        var coinsCount = db.Coins.Count();
                        if (order != null && db.Coins.Any(x => x.CoinId.Equals(order.CoinId)) && order.Name != null &&
                            !order.Name.Equals(""))
                        {
                            //_ordersList.Add(order);
                            db.Orders.Add(order);
                            //using (var db = new BusinessLogic.BusinessLogic())
                            //    db.AddOrder(Mapper.Map<OrderBL>(order));
                        }
                        else { }
                    }

                    db.SaveChanges();
                }
            }
        }
    }
}
