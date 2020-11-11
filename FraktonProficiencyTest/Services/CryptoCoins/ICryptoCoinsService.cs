using FraktonProficiencyTest.Models;

namespace FraktonProficiencyTest.Services.CryptoCoins
{
    public interface ICryptoCoinsService:IService
    {
        CryptoCoinsModel GetAll();
    }
}
