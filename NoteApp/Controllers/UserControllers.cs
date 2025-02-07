using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NoteApp.Data;
using NoteApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace NoteApp.Controllers
{
    public class LoginInfo
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserToken
    {
        public string Email { get; set; }
        public string JWT { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly NewsAppContext _context;
        public UsersController(NewsAppContext context) { _context = context; }


        [HttpPost("login")]
        public IActionResult Login(LoginInfo login)
        {
            User userInDb =
            _context.Users.FirstOrDefault(user => user.Email == login.Email);

            if (userInDb != null
            && HashGenerator.VerifyHash(
            userInDb.Password, login.Password, userInDb.Salt))
            {
                return Ok(CreateToken(userInDb.Id, userInDb.Email));
            }
            return Unauthorized();
        }
        [HttpPost("register")]
        public IActionResult Register(LoginInfo login)
        {
            User userInDb = _context.Users.FirstOrDefault(user => user.Email == login.Email);

            if (userInDb == null)
            {
                string salt;
                string pwHash = HashGenerator.GenerateHash(login.Password, out salt);
                User newUser = new User()
                {
                    Email = login.Email,

                    Password = pwHash,

                    Salt = salt
                };
                _context.Users.Add(newUser);

                _context.SaveChanges();

                return Ok(CreateToken(newUser.Id, newUser.Email));
            }
            return BadRequest();
        }
        private UserToken CreateToken(long userId, string email)
        {
            var expires = DateTime.UtcNow.AddDays(5);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                 new Claim(JwtRegisteredClaimNames.Sub, $"{userId}"),
                 new Claim(JwtRegisteredClaimNames.Email, email),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = expires,
                Issuer = JwtConfiguration.ValidIssuer,
                Audience = JwtConfiguration.ValidAudience,
                SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(JwtConfiguration.IssuerSigningKey)),
            SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            return new UserToken { Email = email, ExpiresAt = expires, JWT = jwtToken };
        }
    }
}