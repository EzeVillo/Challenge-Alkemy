using Challenge.Entities;
using Challenge.Interfaces;
using Challenge.ViewModels.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Controllers
{
    [ApiController]
    [Route(template: "auth")]
    public class AutenticacionController : ControllerBase
    {
        private readonly UserManager<Usuario> _usermanager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IMailService _mailService;

        public AutenticacionController(UserManager<Usuario> usermanager, SignInManager<Usuario> signInManager, IMailService mailService)
        {
            _usermanager = usermanager;
            _signInManager = signInManager;
            _mailService = mailService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserRegisterRequestViewModel User)
        {
            if (await _usermanager.FindByNameAsync(User.name) != null) return BadRequest();

            Usuario Usuario = new Usuario
            {
                UserName = User.name,
                Email = User.email,
                EstaActivo = true
            };
            var Resultado = await _usermanager.CreateAsync(Usuario, User.password);
            if (!Resultado.Succeeded) return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                status = "Error",
                Message = $"User creation failed {string.Join(", ", Resultado.Errors.Select(x => x.Description))}"
            });
            await _mailService.SendMail(User);
            return Ok(new
            {
                status = "Success",
                Message = "User created success"
            });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLoginRequestViewModel User)
        {
            var Resultado = await _signInManager.PasswordSignInAsync(User.name, User.password, false, false);
            if (Resultado.Succeeded)
            {
                var CurrentUser = await _usermanager.FindByNameAsync(User.name);
                if (CurrentUser.EstaActivo)
                {
                    return Ok(await GetToken(CurrentUser));
                }
            }
            return StatusCode(StatusCodes.Status401Unauthorized, new
            {
                status = "Error",
                Message = "El usuario no esta autorizado"
            });
        }
        private async Task<UserLoginResponseViewModel> GetToken(Usuario User)
        {
            var UserRoles = await _usermanager.GetRolesAsync(User);
            var AuthClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, User.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            AuthClaims.AddRange(UserRoles.Select(x => new Claim(ClaimTypes.Role, x)));
            var AuthSignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeySecretaSuperLarga"));
            var Token = new JwtSecurityToken(
                issuer: "https://localhost:5001",
                audience: "https://localhost:5001",
                expires: DateTime.Now.AddHours(1),
                claims: AuthClaims,
                signingCredentials: new SigningCredentials(AuthSignInKey, SecurityAlgorithms.HmacSha256));
            return new UserLoginResponseViewModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(Token),
                ValidoHasta = Token.ValidTo
            };
        }
    }
}
