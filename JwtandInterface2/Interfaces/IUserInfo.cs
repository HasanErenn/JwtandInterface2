using JwtandInterface2.Controllers.Models;
using JwtandInterface2.Models;
using Microsoft.AspNetCore.Mvc;

namespace JwtandInterface2.Interfaces
{
    public interface IUserInfo
    {
        Task<ActionResult<string>> saveName(string name, User user); //User ver admin erisim saglayabilecek
        Task<ActionResult<string>> updateName(string name,User user); //User ve admin erisim saglayabilecek
        Task<ActionResult<string>> changeRole(ChangeRole changeRole); //Sadece CEO erisim saglayabilecek
    }
}
