using CofyDevTest.Core;
using CofyDevTest.UI.Driver;
using Microsoft.Testing.Platform.Services;
using OpenQA.Selenium;

namespace CofyDevTest.UI.Pages
{
    public abstract class BasePage
    {
        private static readonly TestConfiguration TestConfiguration =
            TestBootstrap.Instance.ServiceProvider.GetRequiredService<TestConfiguration>();

        protected static IWebDriver Driver { get; set; } = WebDriverManager.GetWebDriver(TestConfiguration.Browser);

        public void OpenBasePage()
        {
            Driver.Navigate().GoToUrl(TestConfiguration.UIURL);
        }

        public void NavigateToUrl(string relativeUrl)
        {
            Driver.Navigate().GoToUrl($"{TestConfiguration.UIURL}/{relativeUrl}");
        }

        public string CurrentUrl => Driver.Url;
    }
}
