using System;
using System.Collections.Generic;
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
            //DB.Coins.Update(coin);
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
