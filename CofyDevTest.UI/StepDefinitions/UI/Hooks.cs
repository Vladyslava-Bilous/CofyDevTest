using CofyDevTest.UI.Driver;
using Reqnroll;

namespace CofyDevTest.UI.StepDefinitions.UI
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
