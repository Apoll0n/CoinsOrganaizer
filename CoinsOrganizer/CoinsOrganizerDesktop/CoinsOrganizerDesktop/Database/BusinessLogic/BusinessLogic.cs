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

        public void AddCoin(Coin element)
        {
            DB.Coins.Create(element);
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

        public void Save()
        {
            DB.Save();
        }

        public void UpdateCoin(CoinBL element)
        {
            if (element != null && GetCoinsBL().Any(x => x.CoinId.Equals(element.CoinId)))
            {
                DB.Coins.Update(element.CoinDB);
                DB.Save();
            }
            else
            { }
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
            return DB.Coins.ReadAll().ToList();
        }

        public void ApplyChanges()
        {
            DB.IsDirty = true;
        }

        public IEnumerable<CoinBL> GetCoinsBL()
        {
            return DB.CoinsBl;
        }

        public IEnumerable<Order> GetOrders()
        {
            return DB.Orders.ReadAll().ToList();
        }

        public IEnumerable<OrderBL> GetOrdersBL()
        {
            return DB.OrdersBl;
        }

        public void Dispose()
        {
            DB.Dispose();
        }
    }
}
