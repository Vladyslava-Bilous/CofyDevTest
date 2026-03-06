using CofyDevTest.UI.Helpers;
using OpenQA.Selenium;

namespace CofyDevTest.UI.Pages
{
    public class HomePage : BasePage
    {
        public const string Url = "home-page";
        private IWebElement Logo => Driver.FindElement(By.Id("logo"));
        private IWebElement UserProfileButton => Driver.FindElement(By.Id("user-profile"));
        private IWebElement LogoutButton => Driver.FindElement(By.Id("logout"));

        public bool IsUserProfileButtonDisplayed() => UserProfileButton.IsDisplayed();
        public bool IsLogoDisplayed() => Logo.IsDisplayed();

        public void LogOut()
        {
            try
            {
                LogoutButton.Click();
                Driver.Manage().Cookies.DeleteAllCookies();
                ((IJavaScriptExecutor)Driver).ExecuteScript("window.localStorage.clear(); window.sessionStorage.clear();");
                Driver.Navigate().Refresh();
            }
            catch(Exception e)
            {
                throw new Exception("Failed to log out. User is still authenticated.", e);
            }
        }
    }
}
