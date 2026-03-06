using CofyDevTest.UI.Helpers;
using OpenQA.Selenium;

namespace CofyDevTest.UI.Pages
{
    public class LoginPage : BasePage
    {
        public const string Url = "login";
        private IWebElement UsernameInput => Driver.FindElement(By.Id("username"));
        private IWebElement PasswordInput => Driver.FindElement(By.Id("password"));
        private IWebElement LogInButton => Driver.FindElement(By.Id("login"));

        public bool IsUsernameInputDisplayed() => UsernameInput.IsDisplayed();
        public bool IsPasswordInputDisplayed() => PasswordInput.IsDisplayed();
        public bool IsLogInButtonDisplayed() => LogInButton.IsDisplayed();

        public void LogInAs(string userName, string password)
        {
            UsernameInput.SendKeys(userName);
            PasswordInput.SendKeys(password);
            LogInButton.Click();
        }
    }
}
