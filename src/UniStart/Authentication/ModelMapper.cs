namespace UniStart.Authentication
{
    using System.Collections.Generic;
    using Unistart.Models;
    using System.Security.Claims;

    public class ModelMapper
    {
        public UserReturnModel MapToReturnModel(
            User appUser,
            IList<string> roles,
            IList<Claim> claims)
        {
            return new UserReturnModel
            {
                Id = appUser.Id,
                UserName = appUser.UserName,
                FullName = string.Format("{0} {1}", appUser.FirstName, appUser.LastName),
                Email = appUser.Email,
                EmailConfirmed = appUser.EmailConfirmed,
                JoinDate = appUser.JoinDate,
                Level = appUser.Level,
                University = appUser.University,
                Roles = roles,
                Claims = claims
            };
        }
    }
}
