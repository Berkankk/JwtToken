using JwtToken.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JwtToken.Context
{
    public class DbContext : IdentityDbContext<AppUserClass>
    {
        public DbContext(DbContextOptions options) : base(options) { }

        public DbSet<AppUserClass> AppUserClasses { get; set; }
        public DbSet<AuthenticationModel> AuthenticationModels { get; set; }
        public DbSet<Authorization> Authorizations  { get; set; }

    }

}
