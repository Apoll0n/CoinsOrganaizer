using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BusinessLogic.Entities;
using DataAccessLayer;

namespace BusinessLogic
{
    public class BusinessLogic : IDisposable
    {

        private UnitOfWork DB { get; }

        public BusinessLogic()
        {
            DB = new UnitOfWork();
        }

        public void AddCoin(CoinBL element)
        {
            DB.Coins.Create(Mapper.Map<Coin>(element));
            DB.Save();
        }
        //public void AddGame(GameBL element)
        //{
        //    List<Player> players = Mapper.Map<List<Player>>(element.Players);
        //    Stadium place = Mapper.Map<Stadium>(element.Place);

        //    Game game = new Game()
        //    {
        //        Players = players,
        //        Place = place,
        //        Spectators = element.Spectators,
        //        Date = element.Date,
        //        GameResult = (DataAccessLayer.Entities.Result)element.GameResult
        //    };

        //    DB.Games.Create(game);
        //    DB.Save();
        //}
        //public void AddStadium(StadiumBL element)
        //{
        //    DB.Stadiums.Create(Mapper.Map<Stadium>(element));
        //    DB.Save();
        //}

        public void RemoveCoin(int id)
        {
            DB.Coins.Delete(id);
            DB.Save();
        }

        //public void RemoveAllCoins()
        //{
        //    foreach (var coin in DB.Coins.ReadAll())
        //    {
        //        DB.Coins.Delete(coin.Id);
        //    }

        //    DB.Save();
        //}
        //public void RemoveGame(int id)
        //{
        //    DB.Games.Delete(id);
        //    DB.Save();
        //}
        //public void RemoveStadium(int id)
        //{
        //    DB.Stadiums.Delete(id);
        //    DB.Save();
        //}

        public void UpdateCoin(CoinBL element)
        {
            Coin toUpdate = DB.Coins.Read(element.Id);

            if (toUpdate != null)
            {
                toUpdate = Mapper.Map<Coin>(element);
                DB.Coins.Update(toUpdate);
                DB.Save();
            }
        }
        //public void UpdateGame(GameBL element)
        //{
        //    Game toUpdate = DB.Games.Read(element.Id);

        //    List<Player> list = new List<Player>();

        //    foreach (PlayerBL model in element.Players)
        //        list.Add(DB.Players.Read(model.Id));

        //    if (toUpdate != null)
        //    {
        //        toUpdate.Players = list;
        //        toUpdate.Place = DB.Stadiums.Read(element.Id);
        //        toUpdate.Spectators = element.Spectators;
        //        toUpdate.Date = element.Date;
        //        toUpdate.GameResult = toUpdate.GameResult;

        //        DB.Games.Update(toUpdate);
        //        DB.Save();
        //    }
        //}
        //public void UpdateStadium(StadiumBL element)
        //{
        //    Stadium toUpdate = DB.Stadiums.Read(element.Id);

        //    if (toUpdate != null)
        //    {
        //        toUpdate = Mapper.Map<Stadium>(element);
        //        DB.Stadiums.Update(toUpdate);
        //        DB.Save();
        //    }
        //}

        public IEnumerable<CoinBL> GetCoins()
        {
            List<CoinBL> result = new List<CoinBL>();

            foreach (var item in DB.Coins.ReadAll())
                result.Add(Mapper.Map<CoinBL>(item));

            return result;
        }
        //public IEnumerable<GameBL> GetGames()
        //{
        //    List<GameBL> result = new List<GameBL>();

        //    foreach (var item in DB.Games.ReadAll())
        //    {
        //        result.Add(new GameBL
        //        {
        //            Players = Mapper.Map<List<PlayerBL>>(item.Players),
        //            Place = Mapper.Map<StadiumBL>(item.Place),
        //            Date = item.Date,
        //            GameResult = (BusinessLogic.Entities.Result)item.GameResult,
        //            Spectators = item.Spectators,
        //            Id = item.Id
        //        });
        //    }

        //    return result;
        //}
        //public IEnumerable<StadiumBL> GetStadiums()
        //{
        //    List<StadiumBL> result = new List<StadiumBL>();

        //    foreach (var item in DB.Stadiums.ReadAll())
        //        result.Add(Mapper.Map<StadiumBL>(item));

        //    return result;
        //}

        public void Dispose()
        {
            DB.Dispose();
        }
    }
}
