using CofyDevTest.Core.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace CofyDevTest.Core.Driver
{
    public sealed class WebDriverManager
    {
        private static readonly object Sync = new();
        private static IWebDriver? _webDriverInstance;

        public static IWebDriver GetWebDriver(Browser browser)
        {
            lock (Sync)
            {
                if (_webDriverInstance is not null)
                {
                    return _webDriverInstance;
                }

                _webDriverInstance = BuildDriver(browser);
                return _webDriverInstance;
            }
        }

        private static IWebDriver BuildDriver(Browser browser)
        {
            switch (browser)
            {
                case Browser.Chrome:
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("--start-maximized");
                    chromeOptions.AddArgument("--disable-extensions");
                    return new ChromeDriver(ChromeDriverService.CreateDefaultService(), chromeOptions);

                case Browser.Firefox:
                    var firefoxOptions = new FirefoxOptions();
                    firefoxOptions.AddArgument("--start-maximized");
                    return new FirefoxDriver(FirefoxDriverService.CreateDefaultService(), firefoxOptions);

                case Browser.Edge:
                    var edgeOptions = new EdgeOptions();
                    edgeOptions.AddArgument("start-maximized");
                    return new EdgeDriver(EdgeDriverService.CreateDefaultService(), edgeOptions);

                default:
                    throw new NotSupportedException($"Browser '{browser}' is not supported.");
            }
        }

        public static void QuitDriver()
        {
            lock (Sync)
            {
                if (_webDriverInstance is null)
                    return;

                try
                {
                    _webDriverInstance.Quit();
                }
                catch
                {
                    // ignore errors during quit
                }

                try
                {
                    _webDriverInstance.Dispose();
                }
                catch
                {
                    // ignore errors during dispose
                }

                _webDriverInstance = null;
            }
        }
    }
}