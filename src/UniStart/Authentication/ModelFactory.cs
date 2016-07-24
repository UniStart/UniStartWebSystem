namespace UniStart.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Web.Http.Routing;
    using Unistart.Models;

    public class ModelFactory
    {
        private UrlHelper urlHelper;
        private UserManager appUserManager;

        public ModelFactory(HttpRequestMessage request, UserManager userManager)
        {
            urlHelper = new UrlHelper(request);
            appUserManager = userManager;
        }

        public UserReturnModel Create(User appUser)
        {
            return new UserReturnModel
            {
                // Url = urlHelper.Link("GetUserById", new { id = appUser.Id }),
                Id = appUser.Id,
                UserName = appUser.UserName,
                FullName = string.Format("{0} {1}", appUser.FirstName, appUser.LastName),
                Email = appUser.Email,
                EmailConfirmed = appUser.EmailConfirmed,
                JoinDate = appUser.JoinDate,
                Roles = appUserManager.GetRolesAsync(appUser.Id).Result,
                Claims = appUserManager.GetClaimsAsync(appUser.Id).Result
            };
        }

        // TODO: Extract this one
        public class UserReturnModel
        {
            public string Url { get; set; }
            public string Id { get; set; }
            public string UserName { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public bool EmailConfirmed { get; set; }
            public int Level { get; set; }
            public DateTime JoinDate { get; set; }
            public IList<string> Roles { get; set; }
            public IList<System.Security.Claims.Claim> Claims { get; set; }
        }
    }
}