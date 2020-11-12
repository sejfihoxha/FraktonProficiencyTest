using FraktonProficiencyTest.Data.Entities;
using FraktonProficiencyTest.Models;

namespace FraktonProficiencyTest.Services.UserCoinsFavourites
{
    public interface IUserCoinsFavouriteService : IService
    {
        UserCoinsFavourite AddOrRemoveFromFavourite(int userId, UserCoinsFavouriteCreateModel userCoinsFavouriteModel);
        CryptoCoinsModel GetAllFavouriteByUserId(int userId);
    }
}
