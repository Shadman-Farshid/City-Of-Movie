using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityOfMovie.Data.Entities.User;
using Microsoft.EntityFrameworkCore;
using TopLearn.DataLayer.Entities.Wallet;

namespace CityOfMovie.Data.Context
{
   public class CityOfMovieContext:DbContext
    {
        public CityOfMovieContext(DbContextOptions<CityOfMovieContext> options):base(options)
        {
            
        }

        #region User

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }

        #endregion

        #region Wallet
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletType> WalletTypes { get; set; }
        #endregion
    }
}
