
using FraktonProficiencyTest.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FraktonProficiencyTest.Helpers
{
    public class DataContext: DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DbConectionstring"));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserCoinsFavourite> UserCoinsFavourites { get; set; }
    }
}
