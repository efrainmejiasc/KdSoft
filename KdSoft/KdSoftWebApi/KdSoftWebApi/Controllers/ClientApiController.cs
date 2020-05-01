using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using KdSoftWebApi.Engine;
using KdSoftWebApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace KdSoftWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientApiController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserApi login)
        {
            IActionResult response = Unauthorized();
            //AUTENTIFICACION CONTRA DB
            if (!Autentificacion())
            {
                return BadRequest(new AuthorizationFailure { 
                    error = "NO AUTENTICADO"
                });
            }


            string unicoIdentificador = Guid.NewGuid().ToString();
            DateTime time = DateTime.Now;
            DateTime expireTime = time.AddMinutes(EngineData.LifeTime);
            var tokenString = GenerateTokenJwt(login, unicoIdentificador, time, expireTime);
            response = Ok(new
            {
                access_token = tokenString,
                expire_token = expireTime,
                type_token = "Bearer",
                refresh_token = unicoIdentificador,
                email = login.Email,
                user = login.Username,
                password = login.Password
            });

            return response;
        }

        private string GenerateTokenJwt(UserApi user, string unicoIdentificador, DateTime time, DateTime expireTime)
        {
            // appsetting for Token JWT
            var secretKey = EngineData.JwtKey;
            var audienceToken = EngineData.JwtAudience;
            var issuerToken = EngineData.JwtIssuer;
            //
            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // create a claimsIdentity
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.Password),
                new Claim (ClaimTypes.DateOfBirth, DateTime.Now.ToString()),
                new Claim ("HoraToken", DateTime.UtcNow.ToString()),
                new Claim (ClaimTypes.Anonymous, unicoIdentificador)
            });

            // create token to the user
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: audienceToken,
                issuer: issuerToken,
                subject: claimsIdentity,
                notBefore: time,
                expires: expireTime,
                signingCredentials: signingCredentials);

            string token = tokenHandler.WriteToken(jwtSecurityToken);
            return token;
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [ActionName("ClientList")]
        public List<Client> ListClient(int id)
        {
            List<Client> cliente = new List<Client>();

            if (id == 1)
            {
                Client client0 = new Client()
                {
                    Id = 1,
                    Name = "Efrain",
                    LastName = "Mejias"
                };

                Client client1 = new Client()
                {
                    Id = 2,
                    Name = "Juan",
                    LastName = "Arteaga"
                };

                cliente.Add(client0);
                cliente.Add(client1);
            }
            else if (id == 2)
            {
                Client client0 = new Client()
                {
                    Id = 3,
                    Name = "Maria",
                    LastName = "Mejias"
                };

                Client client1 = new Client()
                {
                    Id = 4,
                    Name = "Mariana",
                    LastName = "Arteaga"
                };

                cliente.Add(client0);
                cliente.Add(client1);
            }

            return cliente;

        }
    }

}
