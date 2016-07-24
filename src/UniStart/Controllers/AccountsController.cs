namespace UniStart.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Microsoft.AspNet.Identity;
    using Unistart.Models;
    using System.Collections.Generic;
    using System.Security.Claims;

    public class AccountsController : BaseApiController
    {
        public async Task<IHttpActionResult> CreateUser(CreateUserBindingModel createUserModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                UserName = createUserModel.Username,
                Email = createUserModel.Email,
                FirstName = createUserModel.FirstName,
                LastName = createUserModel.LastName,
                Level = 3,
                University = createUserModel.University,
                JoinDate = DateTime.Now.Date
            };

            IdentityResult addUserResult = 
                await userManager.CreateAsync(user, createUserModel.Password);

            if (!addUserResult.Succeeded)
            {
                return GetErrorResult(addUserResult);
            }
            
            return Ok(modelMapper.MapToReturnModel(user, GetRoles(user.Id), GetClaims(user.Id)));
        }
        
        public IHttpActionResult GetUsers()
        {
            return Ok(userManager.Users
                .ToList().Select(u => 
                    modelMapper.MapToReturnModel(u, GetRoles(u.Id), GetClaims(u.Id))));
        }

        private IList<Claim> GetClaims(string id)
        {
            var userClaims = userManager.GetClaimsAsync(id).Result;
            return userClaims;
        }

        private IList<string> GetRoles(string id)
        {
            var userRoles = userManager.GetRolesAsync(id).Result;
            return userRoles;
        }

        public async Task<IHttpActionResult> GetUserById(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                return Ok(modelMapper.MapToReturnModel(
                    user, GetRoles(user.Id), GetClaims(user.Id)));
            }

            return NotFound();

        }
        
        public async Task<IHttpActionResult> GetUserByName(string username)
        {
            var user = await userManager.FindByNameAsync(username);

            if (user != null)
            {
                return Ok(modelMapper.MapToReturnModel(
                    user, GetRoles(user.Id), GetClaims(user.Id)));
            }

            return NotFound();
        }
    }
}
