using CofyDevTest.Core.Driver;
using Reqnroll;

namespace CofyDevTest.StepDefinitions.UI
{
    [Binding]
    public static class Hooks
    {
        [AfterFeature]
        public static void TearDown()
        {
            WebDriverManager.QuitDriver();
        }
    }
}
