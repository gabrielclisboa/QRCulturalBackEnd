using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QRCulturalBackEnd.AppDbContext;
using QRCulturalBackEnd.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QRCulturalBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Admin login)
        {
            var admin = await _context.Admins.FindAsync(login.usuario);
            if (admin == null || admin.senha != login.senha)
            {
                return Ok(new { success = false });
            }
            if (admin.senha == login.senha && admin.usuario == login.usuario)
            {
                return Ok(new { success = true });
            }
            else
            {
                return Ok(new { success = false });
            }
  
        }

    }
}
