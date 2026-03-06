using CofyDevTest.Core.API.Auth;
using CofyDevTest.Core.API.User;
using CofyDevTest.API.TestData;
using CofyDevTest.Core.API;
using CofyDevTest.Core.API.LoginFailTotal;

namespace CofyDevTest.API.Tests
{
    [TestFixture]
    public class LoginFailTotalTests
    {
        private AuthHandler _adminAuthHandler;
        private LoginFailTotalApi LoginFailTotalApi => new(_adminAuthHandler);

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var adminToken = new AuthenticationApi().GetToken(Users.AdminUser).GetAwaiter().GetResult();
            _adminAuthHandler = new AuthHandler(adminToken);

            VerifyIfUserExists(Users.LoginFail0.UserEmail);
            VerifyIfUserExists(Users.LoginFail1.UserEmail);
            VerifyIfUserExists(Users.LoginFail3.UserEmail);
            VerifyIfUserExists(Users.LoginFail5.UserEmail);
            VerifyIfUserExists(Users.LoginFail10.UserEmail);
        }

        [SetUp]
        public void SetUp()
        {
            FailLoginAs(Users.LoginFail1, 1);
            FailLoginAs(Users.LoginFail3, 3);
            FailLoginAs(Users.LoginFail5, 5);
            FailLoginAs(Users.LoginFail10, 10);
        }

        private void VerifyIfUserExists(string userEmail)
        {
            var userInApp = new UserApi(_adminAuthHandler).GetUser(userEmail).GetAwaiter().GetResult();
            if (userInApp == null)
            {
                //register a user in application
            }
        }

        private void FailLoginAs(UserModel user, int countOfFailedLoginAttempt = 1)
        {
            //login with valid credentials, to make sure that failed login attempts are count correctly
            new AuthenticationApi().GetToken(user).GetAwaiter().GetResult();
            for (var i = 1; i <= countOfFailedLoginAttempt; i++)
            {
                try
                {
                    new AuthenticationApi().GetToken(user.UserEmail, "InvalidPassword").GetAwaiter().GetResult();
                }
                catch (HttpRequestException ex) when (ex.HttpRequestError == HttpRequestError.UserAuthenticationError)
                {
                    TestContext.WriteLine($"User {user.UserEmail} failed login attempt #: {i}");
                }
            }
        }

        [Test]
        public void GetLoginFailTotal_FilterByUserName_ReturnsActualCountOfFailedLoginAttemptsForUser()
        {
            var expectedResponse = new LoginFailTotalUserModel[]
            {
                new() { LoginFailCount = 3, UserEmail = Users.LoginFail3.UserEmail }
            };

            var loginFailTotalResponse = LoginFailTotalApi.GetLoginFailTotal(Users.LoginFail3.UserEmail).GetAwaiter()
                .GetResult();
            Assert.That(loginFailTotalResponse.IsSuccessStatusCode, Is.True);

            var loginFailTotalBody = loginFailTotalResponse.DeserializeContentAsAsync<GetLoginFailTotalResponseModel>()
                .GetAwaiter().GetResult();
            CollectionAssert.AreEquivalent(expectedResponse, loginFailTotalBody!.LoginFailTotalUsers);
        }

        [Test]
        public void GetLoginFailTotal_FilterByFailCount_ReturnsUsersWithFailedLoginAttemptsAboveThreshold()
        {
            var expectedResponse = new LoginFailTotalUserModel[]
            {
                new() { LoginFailCount = 5, UserEmail = Users.LoginFail5.UserEmail },
                new() { LoginFailCount = 10, UserEmail = Users.LoginFail10.UserEmail }
            };

            var loginFailTotalResponse = LoginFailTotalApi.GetLoginFailTotal(failCount: 3).GetAwaiter()
                .GetResult();
            Assert.That(loginFailTotalResponse.IsSuccessStatusCode, Is.True);

            var loginFailTotalBody = loginFailTotalResponse.DeserializeContentAsAsync<GetLoginFailTotalResponseModel>()
                .GetAwaiter().GetResult();
            CollectionAssert.AreEquivalent(expectedResponse, loginFailTotalBody!.LoginFailTotalUsers);
        }

        [Test]
        public void GetLoginFailTotal_AfterResetForTheUser_DoesNotReturnUserInResponse()
        {
            var userEmailToReset = Users.LoginFail5.UserEmail;
            LoginFailTotalApi.ResetLoginFailTotal(userEmailToReset).GetAwaiter().GetResult();
            var loginFailTotalResponse = LoginFailTotalApi.GetLoginFailTotal(failCount: 3).GetAwaiter()
                .GetResult();
            Assert.That(loginFailTotalResponse.IsSuccessStatusCode, Is.True);

            var loginFailTotalBody = loginFailTotalResponse.DeserializeContentAsAsync<GetLoginFailTotalResponseModel>()
                .GetAwaiter().GetResult();
            Assert.That(loginFailTotalBody!.LoginFailTotalUsers!.Any(x => x.UserEmail.Equals(userEmailToReset)), Is.False);
        }

        [Test]
        public void GetLoginFailTotal_AfterResetAndNewFailedLoginAttempts_ReturnsUserDataWithNewCountForFailedLoginAttempts()
        {
            var expectedResponse = new LoginFailTotalUserModel[]
            {
                new() { LoginFailCount = 4, UserEmail = Users.LoginFail10.UserEmail }
            };
            var userToReset = Users.LoginFail10;
            LoginFailTotalApi.ResetLoginFailTotal(userToReset.UserEmail).GetAwaiter().GetResult();
            FailLoginAs(userToReset, 4);
            var loginFailTotalResponse = LoginFailTotalApi.GetLoginFailTotal(userToReset.UserEmail).GetAwaiter()
                .GetResult();
            Assert.That(loginFailTotalResponse.IsSuccessStatusCode, Is.True);

            var loginFailTotalBody = loginFailTotalResponse.DeserializeContentAsAsync<GetLoginFailTotalResponseModel>()
                .GetAwaiter().GetResult();
            CollectionAssert.AreEquivalent(expectedResponse, loginFailTotalBody!.LoginFailTotalUsers);
        }

        [Test]
        public void PutLoginFailTotal_ForUserA_DoesNotAffectOtherUsers()
        {
            var expectedResponse = new LoginFailTotalUserModel[]
            {
                new() { LoginFailCount = 1, UserEmail = Users.LoginFail1.UserEmail },
                new() { LoginFailCount = 5, UserEmail = Users.LoginFail5.UserEmail },
                new() { LoginFailCount = 10, UserEmail = Users.LoginFail10.UserEmail }
            };
            var userToReset = Users.LoginFail3;
            LoginFailTotalApi.ResetLoginFailTotal(userToReset.UserEmail).GetAwaiter().GetResult();
            var loginFailTotalResponse = LoginFailTotalApi.GetLoginFailTotal().GetAwaiter().GetResult();
            Assert.That(loginFailTotalResponse.IsSuccessStatusCode, Is.True);

            var loginFailTotalBody = loginFailTotalResponse.DeserializeContentAsAsync<GetLoginFailTotalResponseModel>()
                .GetAwaiter().GetResult();
            CollectionAssert.AreEquivalent(expectedResponse, loginFailTotalBody!.LoginFailTotalUsers);
        }

        [Test]
        public void UserCanLoginSuccessfully_AfterResettingLoginFailTotal()
        {
            var userToReset = Users.LoginFail5;
            LoginFailTotalApi.ResetLoginFailTotal(userToReset.UserEmail).GetAwaiter().GetResult();
            var loginFailTotalResponse = LoginFailTotalApi.GetLoginFailTotal().GetAwaiter().GetResult();
            Assert.That(loginFailTotalResponse.IsSuccessStatusCode, Is.True);

            var newToken = new AuthenticationApi().GetToken(userToReset).GetAwaiter().GetResult();
            Assert.That(newToken, Is.Not.Null.Or.Empty);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _adminAuthHandler.Dispose();
        }
    }
}