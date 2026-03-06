using CofyDevTest.Core.API.Auth;
using CofyDevTest.UI.Pages;
using Reqnroll;

namespace CofyDevTest.UI.StepDefinitions.UI
{
    [Binding]
    public class LoginStepDefinitions
    {
        private LoginPage LoginPage => new();
        private HomePage HomePage => new();

        [Given("I’m not logged in with a genuine user")]
        public void GivenImNotLoggedInWithAGenuineUser()
        {
            if (!HomePage.IsUserProfileButtonDisplayed())
                return;
            HomePage.LogOut();
        }

        [Given("I’m on the login screen")]
        public void GivenIMOnTheLoginScreen()
        {
            LoginPage.OpenBasePage();
        }

        [When("I navigate to {string} page on the tracking site")]
        public void WhenINavigateToPageOnTheTrackingSite(string page)
        {
            HomePage.NavigateToUrl(page);
        }

        [When("I enter credentials of {string} user")]
        public void WhenIEnterCredentialsOfUser(UserModel user)
        {
            LoginPage.LogInAs(user.UserEmail, user.UserPassword);
        }

        [Then("I am presented with a login screen")]
        public void ThenIAmPresentedWithALoginScreen()
        {
            Assert.Multiple(() =>
            {
                Assert.That(LoginPage.CurrentUrl, Does.EndWith(LoginPage.Url), $"Url is mismatched. Url should be {LoginPage.Url}");
                Assert.That(LoginPage.IsUsernameInputDisplayed, Is.True, "Login input is not displayed.");
                Assert.That(LoginPage.IsPasswordInputDisplayed(), Is.True, "Password input is not displayed.");
                Assert.That(LoginPage.IsLogInButtonDisplayed(), Is.True, "Login button is not displayed.");
            });
        }

        [Then("I am logged in successfully")]
        public void ThenIAmLoggedInSuccessfully()
        {
            Assert.Multiple(() =>
            {
                Assert.That(HomePage.CurrentUrl, Does.EndWith(HomePage.Url), $"Url is mismatched. Url should be {HomePage.Url}");
                Assert.That(HomePage.IsLogoDisplayed(), Is.True, "Application Logo is not displayed.");
                Assert.That(HomePage.IsUserProfileButtonDisplayed(), Is.True, "User profile button is not displayed.");
            });
        }
    }
}
