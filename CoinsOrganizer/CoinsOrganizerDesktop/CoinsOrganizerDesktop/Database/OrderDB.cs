﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinsOrganizerDesktop.Database.DatabaseModels;
using CoinsOrganizerDesktop.DataBase.DbContext;

namespace CoinsOrganizerDesktop.Database
{
    public class OrderDB : ICoinsRepository<Order>
    {
        private CoinsOrganizerContext _context;

        public OrderDB(CoinsOrganizerContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> ReadAll()
        {
            return _context.Orders;
        }

        public Order Read(int id)
        {
            return _context.Orders.Find(id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Create(Order order)
        {
            _context.Orders.Add(order);
        }

        public void Update(Order coin)
        {
            //DB.Coins.Update(coin);
        }

        public void Delete(int id)
        {
            Order order = _context.Orders.Find(id);

            if (order != null)
            {
                _context.Orders.Remove(order);
            }
        }

        public void Delete(Order order)
        {
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
        }
    }
}
