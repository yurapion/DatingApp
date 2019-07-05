
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.DTO;
using DatingApp.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.AspNetCore.Authorization;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UseForRegisterDto userForRegisterDto)
        {
            //validate request
            

            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            if(await _repo.UserExsist(userForRegisterDto.Username)) return BadRequest("Username already exsist");

            var userToCreate = new User
            {
                UserName = userForRegisterDto.Username
            };

            var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password);

            return StatusCode(201);


        }

         [AllowAnonymous]
        [HttpPost("login")]
        public async  Task<IActionResult> Login (UseForLoginrDto userForLoginrDto)
        {
           // throw new Exception("Computer says now");
           var userFromRepo = await _repo.Login(userForLoginrDto.Username.ToLower(), userForLoginrDto.Password);
           if (userFromRepo == null) return Unauthorized();

          var claims = new[]
          {
              new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
              new Claim(ClaimTypes.Name, userFromRepo.UserName)
          };
           

           var key = new SymmetricSecurityKey(Encoding.UTF8
           .GetBytes(_config.GetSection("AppSettings:Token").Value));

           var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

           var tokenDescription = new SecurityTokenDescriptor
           {
               Subject = new ClaimsIdentity(claims),
               Expires = System.DateTime.Now.AddDays(1),
               SigningCredentials = creds
           };

           var tokenHandler = new JwtSecurityTokenHandler();

           var token = tokenHandler.CreateToken(tokenDescription);

           return Ok(new {
               token= tokenHandler.WriteToken(token)
           });
         
        
        }
    }
}