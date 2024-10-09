using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JwtToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController(RoleManager<IdentityRole> roleManager) : ControllerBase
    {
        //Burada ctor geçtik .net8 ile burada geçebiliyoruz 
        [HttpPost]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var role = new IdentityRole
            {
                Name = roleName, //Burada role namde adında bir rol oluşturuyoruz
            };


            var result = await roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return Ok($"{roleName} rolü oluşturuldu");
            }

            return BadRequest("Bir hata oluştu");

        }
    }
}
