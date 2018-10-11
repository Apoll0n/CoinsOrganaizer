﻿using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private Context DB;

        public OrderRepository(Context context)
        {
            DB = context;
        }

        public IEnumerable<Order> ReadAll()
        {
            return DB.Orders;
        }
        public Order Read(int id)
        {
            return DB.Orders.Find(id);
        }
        public void Create(Order order)
        {
            DB.Orders.Add(order);
        }
        public void Update(Order order)
        {
            DB.Orders.Update(order);
        }
        public void Delete(int id)
        {
            Order order = DB.Orders.Find(id);
            if (order != null)
                DB.Orders.Remove(order);
        }
    }
}
