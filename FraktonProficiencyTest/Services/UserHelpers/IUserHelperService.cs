
namespace FraktonProficiencyTest.Services.UserHelpers
{
    public interface IUserHelperService : IService
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
    }
}
