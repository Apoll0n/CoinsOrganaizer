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
        public void Create(Coin game)
        {
            DB.Coins.Add(game);
        }
        public void Update(Coin game)
        {
            DB.Coins.Update(game);
        }
        public void Delete(int id)
        {
            Coin game = DB.Coins.Find(id);
            if (game != null)
                DB.Coins.Remove(game);
        }
    }
}
