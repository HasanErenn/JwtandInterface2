using JwtandInterface2.Models;

namespace JwtandInterface2.Controllers.Models
{
    public class UserInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public UserDto UserDto { get; set; }
        public int UserDtoId { get; set; }
        public List<RoleDto> RoleDtos { get; set; }
        public int RoleId { get; set; } = 6;
    }
}
