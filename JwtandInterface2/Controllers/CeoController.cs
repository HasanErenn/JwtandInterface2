using JwtandInterface2.Controllers.Models;
using JwtandInterface2.Interfaces;
using JwtandInterface2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JwtandInterface2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CeoController : ControllerBase, ICeo
    {
        UserDto userdto = new UserDto();
        RoleDto roleDto = new RoleDto();
        private readonly DataContext _Context;
        public const string roleCeo= "CEO";
        public const string roleAdmin = "admin";
        public const string roleUser = "User";

        public CeoController(DataContext context)
        {
            _Context = context;
        }


        [HttpPost("AddRole"), Authorize(Roles=roleCeo)]
        public async Task<ActionResult<string>> CeoAddTitle(Role role)
        {
            if(checkTitleEmpty(role.Name))
                return BadRequest("Rol bos birakilamaz");
            if (checkRole(role.Name))
                return BadRequest("Ayni isimde bir rol mevcuttur");

            RoleDto newRole = new RoleDto()
            {
                Name = role.Name
            };
            await _Context.Roles.AddAsync(newRole);
            await  _Context.SaveChangesAsync();
            return Ok(role.Name + " isimli rol basariyla kaydedildi");
        }


        [HttpDelete("DeleteRole"), Authorize(Roles = roleCeo)]
        public async Task<ActionResult<string>> CeoDeleteTitle(Role role)
        {
            if (checkTitleEmpty(role.Name))
                return BadRequest("Rol bos birakilamaz");

            var getRole= _Context.Roles.SingleOrDefault(p=>p.Name.ToLower().Equals(role.Name .ToLower()));
            if (getRole==null)
                return BadRequest("Rol bulunamadi!");
            var deleteRole =await _Context.Roles.FindAsync(getRole.Id);
             _Context.Roles.Remove(deleteRole);
            await _Context.SaveChangesAsync();
            return Ok("Rol silinmistir");
        }


        [HttpPut("Update Role {roleName}"), Authorize(Roles = roleCeo)]
        public async Task<ActionResult<string>> CeoPutTitle(string roleName, Role role)
        {
            var getRole = _Context.Roles.SingleOrDefault(p => p.Name.Equals(roleName));
            if (getRole == null)
                return BadRequest("Rol bulunamadi!");
            if (checkTitleEmpty(role.Name))
                return BadRequest("RoleName bos birakilamaz");
            if (checkRole(role.Name))
                return BadRequest("Ayni isimde role mevcut");

            var updateRole =await _Context.Roles.FindAsync(getRole.Id);
            updateRole.Name= role.Name;
            
            await _Context.SaveChangesAsync();
            return Ok(roleName+","+ role.Name +" olarak degistirilmistir");
        }


        [HttpGet("AllUserInfo"), Authorize(Roles = roleCeo+","+ roleAdmin)]
        public async Task<ActionResult<List<UserInfoDto>>> CeoGetUserInfo()
        {
            var userList = from tbl1 in _Context.UserDtos
                       join tbl2 in _Context.UserInfos
                       on tbl1.Id equals tbl2.UserDtoId
                       join tbl3 in _Context.Roles
                       on tbl2.RoleId equals tbl3.Id
                       select new
                       {
                           Username=tbl1.Username,
                           Name=tbl2.Name,
                           Role=tbl3.Name
                       };

            return Ok(userList);            

        }

        [HttpGet("UserInfoById {id}"), Authorize(Roles = roleCeo + "," + roleAdmin)]
        public async Task<ActionResult<List<UserInfoDto>>> CeoGetUserInfoById(int id)
        {
            if (!checkTitleId(id))
                return BadRequest("Belirtilen Id'de kullanici bulunamadi");
            var user = from tbl1 in _Context.UserDtos
                       join tbl2 in _Context.UserInfos
                       on tbl1.Id equals tbl2.UserDtoId
                       join tbl3 in _Context.Roles
                       on tbl2.RoleId equals tbl3.Id
                       where tbl1.Id == id
                       select new
                       {
                           Username = tbl1.Username,
                           Name = tbl2.Name,
                           Role = tbl3.Name
                       };
            return Ok(user);            

        }

        [HttpDelete("DeleteUser {username}"),Authorize(Roles = roleCeo)]
        public async Task<ActionResult<string>> deleteUser(string username)
        {
            if (!checkUsername(username))
                return BadRequest("Kullanici bulunamadi");
            var user = _Context.UserDtos.SingleOrDefault(p=>p.Username.Equals(username));
            _Context.UserDtos.Remove(user);
             await _Context.SaveChangesAsync();
            
            return Ok(username+" isimli kullanici silindi");
            
        }

        [HttpPut("ChangeRole"), Authorize(Roles= roleCeo)]
        public async Task<ActionResult<string>> changeRole(ChangeRole changeRole)
        {
            if (!checkUsername(changeRole.Username))
                return BadRequest("Kullanici bulunamadi");
            if (!checkRole(changeRole.newRoleforUser))
                return BadRequest("Boyle bir rol bulunamadi");

            var user=  _Context.UserInfos.SingleOrDefault(p=>p.UserDto.Username.Equals(changeRole.Username));
            var newRole =  _Context.Roles.SingleOrDefault(p => p.Name.Equals(changeRole.newRoleforUser));
            user.RoleId = newRole.Id;

           await _Context.SaveChangesAsync();
            return Ok(user.Name+"'in rolu"+newRole.Name+" olarak degistirilmistir");
        }

        private bool checkRole(string role)
        {
          return _Context.Roles.Any(p=>p.Name.ToLower().Equals(role.ToLower()));
        }

        private bool checkTitleEmpty(string title)
        {
            return string.IsNullOrEmpty(title);
        }

        private bool checkTitleId(int title)
        {
           return _Context.UserDtos.Any(p => p.Id.Equals(title));
        }

        private bool checkUsername(string username)
        {
            return _Context.UserDtos.Any(p=>p.Username.ToLower().Equals(username.ToLower()));
        }
    }
}
