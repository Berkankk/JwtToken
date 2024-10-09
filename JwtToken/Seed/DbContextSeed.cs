using JwtToken.Models;
using Microsoft.AspNetCore.Identity;

namespace JwtToken.Seed
{
    public class DbContextSeed
    {
        public static async Task SeedEssentialAsync(UserManager<AppUserClass> userManager,
           RoleManager<IdentityRole> roleManager)
        {
            //Seed roles
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Administrator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Moderator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.User.ToString()));
            //Seed Default User
            var defaultUser = new AppUserClass
            {
                UserName = Authorization.default_username,
                Email = Authorization.default_email,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                await userManager.CreateAsync(defaultUser, Authorization.default_password);
                await userManager.AddToRoleAsync(defaultUser, Authorization.default_role.ToString());
            }
        }
    }
}