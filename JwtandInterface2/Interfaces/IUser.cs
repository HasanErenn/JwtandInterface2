using JwtandInterface2.Controllers.Models;
using JwtandInterface2.Models;
using Microsoft.AspNetCore.Mvc;

namespace JwtandInterface2
{
    public interface IUser
    {
        Task<ActionResult<string>> Register(RegisterForUser user);
        Task<ActionResult<string>> Login(User user);

        Task<ActionResult<string>> changePassword(ChangePassword changePassword);
    }
}
