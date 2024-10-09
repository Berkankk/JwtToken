using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtToken.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "User, Moderator")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProtectedController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetData()
        {
            return Ok("Sadece yetkiniz varsa burayı görebilirsiniz.");
        }
    }
}
//Burası token ile kullanıcı veya moderatör rollerine sahip olması gerektiğini ifade controllerımız