using OpenQA.Selenium;

namespace CofyDevTest.UI.Helpers
{
    public static class WebElementExtensions
    {
        public static bool IsDisplayed(this IWebElement webElement)
        {
            try
            {
                var isDisplayed = webElement.Displayed;
                return isDisplayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
