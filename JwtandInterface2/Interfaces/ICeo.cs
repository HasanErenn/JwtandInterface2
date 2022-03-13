using JwtandInterface2.Controllers.Models;
using JwtandInterface2.Models;
using Microsoft.AspNetCore.Mvc;

namespace JwtandInterface2.Interfaces
{
    public interface ICeo
    {
      Task<ActionResult<string>> CeoAddTitle(Role role);
      Task<ActionResult<string>> CeoDeleteTitle(Role role);
      Task<ActionResult<string>> CeoPutTitle(string roleName, Role role);
      Task <ActionResult<List<UserInfoDto>>> CeoGetUserInfo();
      Task<ActionResult<List<UserInfoDto>>> CeoGetUserInfoById(int id);
      Task<ActionResult<string>> changeRole(ChangeRole changeRole);
      Task<ActionResult<string>> deleteUser(string username);

    }
}
