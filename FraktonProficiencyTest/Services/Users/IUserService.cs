using FraktonProficiencyTest.Data.Entities;
using FraktonProficiencyTest.Models;

namespace FraktonProficiencyTest.Services.Users
{
    public interface IUserService: IService
    {
        User Authenticate(string username, string password);
        User GetById(int id);
        User Create(RegisterModel registerModel, string password);
        void Update(User user);
        User FindByEmail(string email);
    }
}
