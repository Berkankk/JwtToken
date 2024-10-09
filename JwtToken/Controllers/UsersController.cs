using JwtToken.Models;
using JwtToken.ServicesUserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JwtToken.Controllers
{
    [AllowAnonymous] //Bunu yazmamızın sebebi yetkilendirme gerektirmediğini söylüyoruz unutma!!
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService _userService,UserManager<AppUserClass> _userManager) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserDto createUserDto)
        {
            var result = await _userService.RegisterAsync(createUserDto);
            return Ok(result);
            //Bu metot içerisinde kullanıcımızın kayıt işlemlerini yapıyoruz
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(TokenRequestModel model)
        {
            var succeed = await _userService.Login(model);

            if (succeed)
            {
                return Ok("Kullanıcı Sisteme Giriş başarılı");
            }

            return BadRequest("Kullanıcı adı veya şifre hatalı");
            //Burada ise giriş işlemini geçtik
        }

        [HttpPost("createRole")]
        public async Task<IActionResult> CreateRole(CreateRoleDto createRoleDto) //Yeni rolümüzü oluşturuyoruz
        {
            var result = await _userService.AddRoleAsync(createRoleDto);
            return Ok(result);
            
        }

        [HttpGet("token")]
        public async Task<IActionResult> GetToken()
        {


            var currentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserID != null)
            {
                var user = await _userManager.FindByIdAsync(currentUserID);
                var result = await _userService.GetAccessToken(user);

                return Ok(result);

                //Giriş yapan kullanıcının idsini aldık ve userservice ile tokenini oluşturduk
            }

            return NotFound("Giriş yapan kullanıcı bulunamadı");




        }
    }
}
