using AutoMapper;
using FraktonProficiencyTest.Data.Entities;
using FraktonProficiencyTest.Helpers;
using FraktonProficiencyTest.Models;
using FraktonProficiencyTest.Services.UserHelpers;
using System.Linq;


namespace FraktonProficiencyTest.Services.Users
{
    public class UserService : IUserService
    {
        private DataContext _context;
        private readonly IUserHelperService _userHelperService;
        private readonly IMapper _mapper;

        public UserService(DataContext context,IMapper mapper, IUserHelperService userHelperService)
        {
            _context = context;
            _userHelperService = userHelperService;
            _mapper = mapper;
        }
        public User Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users.SingleOrDefault(x => x.Email == email);

            if (user == null || !_userHelperService.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public User Create(RegisterModel model , string password)
        {
            var user = _mapper.Map<User>(model);

            byte[] passwordHash, passwordSalt;

            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_context.Users.Any(x => x.Email == user.Email))
                throw new AppException("Username \"" + user.Email + "\" is already taken");

            _userHelperService.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public User FindByEmail(string email)
        {
            return _context.Users.SingleOrDefault(x => x.Email == email);
        }
    }
}
