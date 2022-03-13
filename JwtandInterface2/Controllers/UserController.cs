using JwtandInterface2.Controllers.Models;
using JwtandInterface2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JwtandInterface2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase, IUser
    {
        UserDto userdto=new UserDto();
        RoleDto roleDto = new RoleDto();
        private readonly DataContext _Context;
        private readonly IConfiguration _Configuration;
        public const string roleCeo = "CEO";
        public const string roleAdmin = "admin";
        public const string roleUser = "User";

        public UserController(DataContext context, IConfiguration configuration)
        {
            _Context = context;
            _Configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(RegisterForUser user)
        {
            if (checkUsernameEmpty(user.Username))
                return BadRequest("Kullanici adi bos birakilamaz");
            if (existUser(user.Username))
                return BadRequest("Kullanici mevcut");
            if (checkPasswordEmpty(user.Password))
                return BadRequest("Sifre kismi bos birakilamaz");
            if(checkUsernameEmpty(user.Name))
                return BadRequest("Isim bos birakilamaz");


            CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

            UserDto newUser = new UserDto()
            {
                Username = user.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await _Context.UserDtos.AddAsync(newUser);
            await _Context.SaveChangesAsync();

            var name = _Context.UserDtos.SingleOrDefault(p => p.Username.Equals(user.Username));

            UserInfoDto newUserInfo = new UserInfoDto()
            {
                Name = user.Name,
                UserDtoId = name.Id
            };
            await _Context.UserInfos.AddAsync(newUserInfo);
            await _Context.SaveChangesAsync();

            return Ok(user.Username + " adli hesap basariyla olusturuldu.");            
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(User user)
        {
            if (checkUsernameEmpty(user.Username))
                return BadRequest("Kullanici adi bos birakilamaz");

            var checkUser = _Context.UserDtos.SingleOrDefault(p => p.Username.Equals(user.Username));
            if (checkUser == null)
                return BadRequest("Kullanici bulunamadi");

            if (checkPasswordEmpty(user.Password))
                return BadRequest("Sifre kismi bos birakilamaz");
            if (!verifyPasswordHash(user.Password, checkUser.PasswordHash, checkUser.PasswordSalt))
            return BadRequest("Sifreniz hatali");

            var findUser = _Context.UserInfos.SingleOrDefault(p=>p.UserDtoId.Equals(checkUser.Id));
            var userRole = _Context.Roles.SingleOrDefault(p => p.Id.Equals(findUser.RoleId));
            string getUserRole = userRole.Name;
            var token = CreateToken(user, getUserRole);

            return Ok(token);
        }

        [HttpPut("ChangePassword"), Authorize(Roles= roleCeo+","+roleAdmin + "," +roleUser)]
        public async Task<ActionResult<string>> changePassword(ChangePassword changePassword)
        {
            if (!existUser(changePassword.Username))
                return BadRequest("Kullanici bulunamadi");

            var checkUser = _Context.UserDtos.SingleOrDefault(p => p.Username.Equals(changePassword.Username));
            if (!verifyPasswordHash(changePassword.Password, checkUser.PasswordHash, checkUser.PasswordSalt))
                return BadRequest("Sifreniz hatali");

            if (checkPasswordEmpty(changePassword.newPassword))
                return BadRequest("yeni password bos birakilamaz");

            CreatePasswordHash(changePassword.newPassword, out byte[] passwordHash, out byte[] passwordSalt);
            checkUser.PasswordHash = passwordHash;
            checkUser.PasswordSalt = passwordSalt;

            await _Context.SaveChangesAsync();
            return Ok("Sifreniz basariyla degisti");
        }

        private bool existUser(string username)
        {
            return _Context.UserDtos.Any(p => p.Username.ToLower().Equals(username));
        }
        
        private bool checkUsernameEmpty(string username)
        {
            return string.IsNullOrEmpty(username);
        }
        private bool checkPasswordEmpty(string password)
        {
            return string.IsNullOrEmpty(password);
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac= new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        private bool verifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac= new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user,string getUserRole)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Role, getUserRole)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                (_Configuration.GetSection("AppSettings:Token").Value));
            var creds= new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var token = new JwtSecurityToken(
                claims:claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );
            var jwt= new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
