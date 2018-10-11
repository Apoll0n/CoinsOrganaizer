using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Entities;
using CoinsOrganizer.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Mvc;

namespace CoinsOrganizer.Controlers
{
    public class OrdersController : Controller
    {
        public static int currentEditionID;

        public IActionResult AddPage()
        {
            return View();
        }
        public IActionResult AddOrder(string title, int cost, string link)
        {
            OrderModel order = new OrderModel()
            {
                //Name = title,
                //Link = link,
                //Cost = cost
            };

            using (var db = new BusinessLogic.BusinessLogic())
                db.AddOrder(Mapper.Map<OrderBL>(order));

            return Redirect("~/Home/Index");
        }

        public IActionResult SearchPage()
        {
            //SpreadsheetsService myService = new SpreadsheetsService("CoinsOrganizer");
            //myService.setUserCredentials("tbaton@gmail.com", "POBEDAnotabt9865122879");

            //SpreadsheetQuery query = new SpreadsheetQuery();
            //SpreadsheetFeed feed = myService.Query(query);

            //Console.WriteLine("Your spreadsheets: ");
            //foreach (SpreadsheetEntry entry in feed.Entries)
            //{
            //    Console.WriteLine(entry.Title.Text);
            //}

            DeleteAllOrders();

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
            String range = "'Висилка'!A909:I971";
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
                    OrderModel orders = new OrderModel();

                    if (row.Count == 9)
                    {
                        var index = int.Parse(row[8].ToString() == "." ? "0" : row[8].ToString());
                        var title = row[0].ToString();
                        var paid = row[1].ToString() == "т";
                        double price = 0d;

                        var isDollar = row[4].ToString().Contains("$");
                        var isZloty = row[4].ToString().Contains("zł");
                        //if (row[4].ToString() != "")
                        //{
                        //    price = double.Parse(row[4].ToString().Replace("$.", "").Replace(",", "").Replace(" zł", ""));
                        //}

                        var email = row[3].ToString();
                        var nickname = row[2].ToString();

                        double soldprice = 0d;
                        if (row[4].ToString() != "")
                        {
                            soldprice = double.Parse(row[4].ToString().Replace(" zł", "").Replace(",", "").Replace("$", ""));
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

                        orders = new OrderModel()
                        {
                            NickName = nickname,
                            IsPaid = paid,
                            TrackNumber = trackNumber,
                            OrderDetails = orderdetails,
                            Email = email,
                            OrderId = index,
                            Name = title,
                            SalePrice = soldprice,
                            SaleCurrency = isZloty ? "zł" : "$",
                            WhereSold = isZloty ? "Allegro" : "Ebay",
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
                    if (orders.Link == null)
                    {

                    }
                    if (orders.OrderId != 1018 && orders.OrderId !=0)
                    {
                        


                    using (var db = new BusinessLogic.BusinessLogic())
                        db.AddOrder(Mapper.Map<OrderBL>(orders));
                    }
                }
            }
            //else
            //{
            //    Console.WriteLine("No data found.");
            //}
            //Console.Read();

            return View();
        }

        public IActionResult SearchResult()
        {
            List<OrderModel> orders = new List<OrderModel>();

            using (var db = new BusinessLogic.BusinessLogic())
            {
                foreach (OrderModel order in Mapper.Map<List<OrderModel>>(db.GetOrders()))
                {
                    //if (order.Name == title && order.Link == link)
                    orders.Add(order);
                }
            }
            return View("ShowAllPage", orders);
        }

        public IActionResult ShowAllPage()
        {
            List<OrderModel> list = null;

            using (var db = new BusinessLogic.BusinessLogic())
                list = Mapper.Map<List<OrderModel>>(db.GetOrders());
            list.Reverse();
            return View(list);
        }
        public IActionResult Delete(int id)
        {
            using (var db = new BusinessLogic.BusinessLogic())
                db.RemoveOrder(id);

            return Redirect("~/Home/Index");
        }
        public IActionResult DeleteAllOrders()
        {
            using (var db = new BusinessLogic.BusinessLogic())
            {
                foreach (OrderModel order in Mapper.Map<List<OrderModel>>(db.GetOrders()))
                    db.RemoveCoin(order.OrderId);
            }

            return Redirect("~/Home/Index");
        }

        public IActionResult EditPage(int id)
        {
            OrderModel order = null;

            using (var db = new BusinessLogic.BusinessLogic())
            {
                var orders = Mapper.Map<List<OrderModel>>(db.GetOrders());
                foreach (OrderModel model in orders)
                {
                    if (model.OrderId == id)
                    {
                        order = model;
                        break;
                    }
                }
            }

            if (order != null)
            {
                //currentEditionID = order.Id;
                //ViewData["CoinName"] = order.Name;
                //ViewData["CoinCost"] = order.Cost;
                //ViewData["CoinLink"] = order.Link;
                //ViewData["CoinDollarPrice"] = order.DollarPrice;
                //ViewData["CoinZlotyPrice"] = order.ZlotyPrice;
                //ViewData["CoinIsSold"] = order.IsSold;
                //ViewData["CoinEnglishName"] = order.EnglishName;
                //ViewData["CoinPolishName"] = order.PolishName;
            }

            return View();
        }
        public IActionResult SaveChanges(int id, string name, string surname, string year, bool health, int salary)
        {
            OrderModel order = new OrderModel()
            {
                //Id = currentEditionID,
                //Name = name,
            };

            using (var db = new BusinessLogic.BusinessLogic())
                db.UpdateOrder(Mapper.Map<OrderBL>(order));

            return Redirect("~/Home/Index");
        }
    }
}
