using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using CoinsOrganizerDesktop.Database.DatabaseModels;
using CoinsOrganizerDesktop.DataBase.DbContext;

namespace CoinsOrganizerDesktop.Database
{
    public class CoinsDB : ICoinsRepository<Coin>
    {
        private CoinsOrganizerContext _context;

        public CoinsDB(CoinsOrganizerContext context)
        {
            _context = context;
        }

        public BindingList<Coin> BindingList()
        {
            _context.Coins.Load();
            return _context.Coins.Local.ToBindingList();
        }

        public IEnumerable<Coin> ReadAll()
        {
            return _context.Coins;
        }

        public Coin Read(int id)
        {
            return _context.Coins.Find(id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Create(Coin coin)
        {
            _context.Coins.Add(coin);
        }

        public void Update(Coin coin)
        {
            _context.Entry(coin).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Coin coin = _context.Coins.Find(id);
            if (coin != null)
            {
                _context.Coins.Remove(coin);
            }
        }
        public void Delete(Coin coin)
        {
            if (coin != null)
            {
                _context.Coins.Remove(coin);
            }
        }
    }
}
