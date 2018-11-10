using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CoinsOrganizerDesktop.Database.DatabaseModels;

namespace CoinsOrganizerDesktop.Database.BusinessLogic
{
    public class BusinessLogic : IDisposable
    {
        private UnitOfWork DB { get; }

        public BusinessLogic()
        {
            DB = new UnitOfWork();
        }

        public void AddOrder(OrderBL element)
        {
            DB.Orders.Create(Mapper.Map<Order>(element));
            DB.Save();
        }

        public void AddCoin(CoinBL element)
        {
            DB.Coins.Create(Mapper.Map<Coin>(element));
            DB.Save();
        }

        public void RemoveCoin(int id)
        {
            DB.Coins.Delete(id);
            DB.Save();
        }

        public void RemoveOrder(int id)
        {
            DB.Orders.Delete(id);
            DB.Save();
        }

        public void UpdateCoin(CoinBL element)
        {
            Coin toUpdate = DB.Coins.Read(element.CoinId);

            if (toUpdate != null)
            {
                toUpdate = Mapper.Map<Coin>(element);
                DB.Coins.Update(toUpdate);
                DB.Save();
            }
        }

        public void UpdateOrder(OrderBL element)
        {
            Order toUpdate = DB.Orders.Read(element.OrderId);

            if (toUpdate != null)
            {
                toUpdate = Mapper.Map<Order>(element);
                DB.Orders.Update(toUpdate);
                DB.Save();
            }
        }

        public IEnumerable<Coin> GetCoins()
        {
            List<CoinBL> result = new List<CoinBL>();

            //foreach (var item in DB.Coins.ReadAll())
            //{
            //    var coinBl = new CoinBL
            //    {
            //        AversFotoLink = item.AversFotoLink,
            //        CoinId = item.CoinId,
            //        Cost = item.Cost,
            //        DollarPrice = item.DollarPrice,
            //        EnglishName = item.EnglishName,
            //        IsInStock = item.IsInStock,
            //        IsSold = item.IsSold,
            //        Link = item.Link,
            //        Name = item.Name,
            //        OrderId = item.OrderId,
            //        PolishName = item.PolishName,
            //        ReversFotoLink = item.ReversFotoLink,
            //        ZlotyPrice = item.ZlotyPrice
            //    };

            //    coinBl.Order = new List<OrderBL>();
                  
            //    foreach (var order in item.Order)
            //    {
            //        coinBl.Order.Add(new OrderBL
            //        {
            //            OrderId = order.OrderId,
            //            Coin = order.Coin,
            //            Email = order.Email,
            //            CoinId = order.CoinId,
            //            Name = order.Name,
            //            Link = order.Link,
            //            IsPaid = order.IsPaid,
            //            NickName = order.NickName,
            //            OrderDetails = order.OrderDetails,
            //            SaleCurrency = order.SaleCurrency,
            //            SalePrice = order.SalePrice,
            //            TrackNumber = order.TrackNumber,
            //            WhereSold = order.WhereSold,
            //        });
            //    }

            //    result.Add(Mapper.Map<CoinBL>(item));
            //}

            return DB.Coins.ReadAll().ToList();
        }

        public IEnumerable<OrderBL> GetOrders()
        {
            List<OrderBL> result = new List<OrderBL>();

            foreach (var item in DB.Orders.ReadAll())
                result.Add(Mapper.Map<OrderBL>(item));

            return result;
        }

        public void Dispose()
        {
            DB.Dispose();
        }
    }
}
