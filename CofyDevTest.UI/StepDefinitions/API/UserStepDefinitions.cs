using CofyDevTest.Core.API.Auth;
using CofyDevTest.Core.API.User;
using CofyDevTest.UI.TestData;
using Reqnroll;

namespace CofyDevTest.UI.StepDefinitions.API
{
    [Binding]
    public class UserStepDefinitions
    {
        [Given("{string} user is already registered")]
        public void GivenUserIsAlreadyRegistered(UserModel user)
        {
            var adminToken = new AuthenticationApi().GetToken(Users.AdminUser).GetAwaiter().GetResult();
            var userInApp = new UserApi(new AuthHandler(adminToken)).GetUser(user.UserEmail).GetAwaiter().GetResult();
            if (userInApp == null || !userInApp.IsValid)
            {
                //register a user in application, or make it valid
            }
        }
    }
}
