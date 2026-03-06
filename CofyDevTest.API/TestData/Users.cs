using CofyDevTest.Core.API.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CofyDevTest.API.TestData
{
    public static class Users
    {
        public static UserModel AdminUser = new()
        {
            UserEmail = "admin@cofy.com",
            UserPassword = "admin123-"
        };

        public static UserModel LoginFail0 = new()
        {
            UserEmail = "login_fail_0@cofy.com",
            UserPassword = "loginfail0123-"
        };

        public static UserModel LoginFail1 = new()
        {
            UserEmail = "login_fail_1@cofy.com",
            UserPassword = "loginfail1123-"
        };
        
        public static UserModel LoginFail3 = new()
        {
            UserEmail = "login_fail_3@cofy.com",
            UserPassword = "loginfail3123-"
        };
        
        public static UserModel LoginFail5 = new()
        {
            UserEmail = "login_fail_5@cofy.com",
            UserPassword = "loginfail5123-"
        };
        
        public static UserModel LoginFail10 = new()
        {
            UserEmail = "login_fail_10@cofy.com",
            UserPassword = "loginfail10123-"
        };
    }
}
