namespace UniStart.Authentication
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using UniStart.Data;
    using Microsoft.AspNet.Identity;
    using Unistart.Models;

    public class UserManager : UserManager<User>
    {
        public UserManager(IUserStore<User> store)
            : base(store)
        {
        }

        public static UserManager CreateUserManager()
        {
            var appDbContext = new UniStartContext();
            var appUserManager = new UserManager(new UserStore<User>(appDbContext));

            return appUserManager;
        }
    }
}
