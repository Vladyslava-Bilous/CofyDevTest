using CofyDevTest.Core.API.Auth;

namespace CofyDevTest.UI.TestData
{
    //Users' data may be passed in another way, if it's differ for different environments.
    //For example, from config files.
    //But for the sake of simplicity, I will keep it here.
    public static class Users
    {
        public static UserModel AdminUser = new()
        {
            UserEmail = "admin@cofy.com",
            UserPassword = "admin123-"
        };

        public static UserModel RegularUser = new()
        {
            UserEmail = "regular_visitor@cofy.com",
            UserPassword = "visitor123-"
        };
    }
}
