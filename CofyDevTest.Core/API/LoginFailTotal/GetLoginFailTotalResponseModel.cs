namespace CofyDevTest.Core.API.LoginFailTotal
{
    public class GetLoginFailTotalResponseModel
    {
        public LoginFailTotalUserModel[]? LoginFailTotalUsers { get; set; }
    }

    public class LoginFailTotalUserModel
    {
        public int LoginFailCount { get; set; }
        public string UserEmail { get; set; }
      
    }
}
