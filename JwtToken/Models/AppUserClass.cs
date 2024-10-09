using Microsoft.AspNetCore.Identity;

namespace JwtToken.Models
{
    public class AppUserClass : IdentityUser
    {
        public string Name { get; set; }  //kulanıcımın adı ve soyadını aldım 
        public string Surname { get; set; }
    }
}
