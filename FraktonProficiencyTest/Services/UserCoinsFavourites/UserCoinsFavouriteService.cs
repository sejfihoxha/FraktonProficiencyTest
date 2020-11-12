using AutoMapper;
using FraktonProficiencyTest.Data.Entities;
using FraktonProficiencyTest.Helpers;
using FraktonProficiencyTest.Models;
using FraktonProficiencyTest.Services.CryptoCoins;
using System.Collections.Generic;
using System.Linq;


namespace FraktonProficiencyTest.Services.UserCoinsFavourites
{
    public class UserCoinsFavouriteService : IUserCoinsFavouriteService
    {
        private DataContext _context;
        private readonly IMapper _mapper;
        private readonly ICryptoCoinsService _cryptoCoinsService;

        public UserCoinsFavouriteService(DataContext context, IMapper mapper, ICryptoCoinsService cryptoCoinsService)
        {
            _context = context;
            _mapper = mapper;
            _cryptoCoinsService = cryptoCoinsService;
        }
        public UserCoinsFavourite AddOrRemoveFromFavourite(int userId, UserCoinsFavouriteCreateModel userCoinsFavouriteModel)
        {

            var userCoinsFavourite = _mapper.Map<UserCoinsFavourite>(userCoinsFavouriteModel);
          
            userCoinsFavourite.UserId = userId;

            var exist = _context.UserCoinsFavourites
                                .FirstOrDefault(x => x.UserId == userId && x.CoinId == userCoinsFavourite.CoinId);

            if (exist != null)
            {
                _context.UserCoinsFavourites.Remove(exist);
                _context.SaveChanges();
            }
            else
            {
                _context.UserCoinsFavourites.Add(userCoinsFavourite);
                _context.SaveChanges();
            }

            return userCoinsFavourite;

        }

        public CryptoCoinsModel GetAllFavouriteByUserId(int userId)
        {
            var favoriteCoins = _context.UserCoinsFavourites
                                        .Where(x => x.UserId == userId)
                                        .Select(x => x.CoinId).ToList();

            var ids = string.Join(",",favoriteCoins);

            return _cryptoCoinsService.GetAllFavourites(ids);
        }
    }
}
