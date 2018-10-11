using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories
{
    public class CoinRepository : IRepository<Coin>
    {
        private Context DB;

        public CoinRepository(Context context)
        {
            DB = context;
        }

        public IEnumerable<Coin> ReadAll()
        {
            return DB.Coins;
        }
        public Coin Read(int id)
        {
            return DB.Coins.Find(id);
        }
        public void Create(Coin coin)
        {
            DB.Coins.Add(coin);
        }
        public void Update(Coin coin)
        {
            DB.Coins.Update(coin);
        }
        public void Delete(int id)
        {
            Coin coin = DB.Coins.Find(id);
            if (coin != null)
                DB.Coins.Remove(coin);
        }
    }
}
