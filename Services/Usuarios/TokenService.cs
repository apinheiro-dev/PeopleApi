using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using PeopleApi.Models;

namespace PeopleApi.Services.Usuarios
{
    public class TokenService
    {
        private IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(Usuario usuario)
        {
            Claim[] claims = new Claim[]
            {
                //new Claim("username", usuario.UserName),
                new Claim("username", usuario.UserName),
                new Claim("id", usuario.Id),
                new Claim(ClaimTypes.DateOfBirth, usuario.DataNascimento.ToString()),
                new Claim("loginTimestamp", DateTime.UtcNow.ToString())
            };

            //var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SymmetricSecurityKey"]));

            //var chave = new SymmetricSecurityKey(
            //    Encoding.ASCII.GetBytes(WebApplication.CreateBuilder().Configuration["jwt:secretKey"]));

            var chave = new SymmetricSecurityKey(
                //Encoding.ASCII.GetBytes(WebApplication.Create().Configuration["jwt:secretKey"])
                Encoding.UTF8.GetBytes("e24ac5962e794a78a003efa27de3613c")
                );

            var signingCredentials =
                new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                    expires: DateTime.Now.AddMinutes(30),
                    claims: claims,
                    signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
