using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Interface;
using API.Service;
using API.DTO;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController(DataContext context, ITokenService tokenService) : BaseApiController
    {
        [HttpPost("register")]// account/register
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            if (await UserExists(registerDTO.Username)) return BadRequest("Username already taken.");

            using var hmac = new HMACSHA3_512();
            var user = new AppUser
            {
                UserName = registerDTO.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt = hmac.Key
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();
            return new UserDTO
            {
                Username = user.UserName,
                Token = tokenService.CreateToken(user)
            };
            // return user;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await context.Users.FirstOrDefaultAsync(x =>
                    x.UserName == loginDTO.Username.ToLower());
            if (user == null) return Unauthorized("Invalid UserName");

            using var hmac = new HMACSHA3_512(user.PasswordSalt);

            var ComputeHash = hmac.ComputeHash((Encoding.UTF8.GetBytes(loginDTO.Password)));
            for (int i = 0; i < ComputeHash.Length; i++)
            {
                if (ComputeHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }

            return new UserDTO
            {
                Username = user.UserName,
                Token = tokenService.CreateToken(user)
            };
            // return user;
        }

        private async Task<bool> UserExists(string username)
        {
            return await context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
        }
    }
}
