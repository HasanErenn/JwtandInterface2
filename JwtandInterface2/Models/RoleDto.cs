using JwtandInterface2.Controllers.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwtandInterface2.Models
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public  List<UserInfoDto> UserInfoDtos { get; set; }
    }
}
