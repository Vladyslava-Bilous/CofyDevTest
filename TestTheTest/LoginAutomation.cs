using System;//unused -> remove
using NUnit.Framework;//unused -> remove
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox; //unused -> remove

namespace TestTheTest
{
    public class LoginAutomation
    {
        //all test body is in set up method -> move actions and assert to test method
        // set up method can not be private -> make public
        [SetUp]
        private void Login()
        {
            //unused variable -> remove.
            //With a newer version of Selenium, you don't need to specify the driver path if you're using WebDriverManager.
            string strDriverPath = "path of driver";
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.maximise(); //spelling of the method name is incorrect -> fix to Maximize
            driver.Navigate().GoToUrl("https://qa.sorted.com/newtrack");
            Thread.Sleep(5000); //Bad practice. Better to wait some specific condition on the page. -> replace with explicit wait
            IWebElement user = driver.FindElement(By.Id("//form[@id='loginForm']/input[1]")); // XPath is incorrect for finding element by Id -> fix to By.XPath or change the locator strategy to find element by Id.
            IWebElement password = driver.FindElement(By.XPath("//form[@id='loginForm']/input[2]")); //Will work, but xpath is not the best. Ask FE team to add automation attribute or ids to the inputs.
            IWebElement login = driver.find_element_by_xpath("submit")); //find_element_by_xpath - does not exist -> replace with native FindElement(By.XPath(""))
            username.SendKeys(usernameValue) //username is not defined, I guess it should be user. Semicolon is missed at the end of line.
            password.SendKeys(passwordValue);
            string usernameValue = "john_smith@sorted.com"; //Should be defined before using (before line 26). Can be a const
            string passwordValue = "Pa55w0rd!"""; //Should be defined before using (before line 26). Can be a const. Incorrect using of quotes -> fix to "Pa55w0rd!" or "Pa55w0rd!\"\""
            string actualUrl = "http://qa.sorted.com/newtrack/loginSuccess"; //This is not actual url, but expected. Variable name is misleading -> change to expectedUrl
            string expectedURL = driver.Url; //This is actual url. Variable name is misleading -> change to actualUrl
            Assert.Equals(actualUrl, expectedURL); //Equals - is not a valid assertion method in NUnit. Also, expected and actual values are mixed up -> change to Assert.That(actualUrl, Is.EqualTo(expectedUrl));
        }

        [TearDown]
        public void Teardown()
        {
            driver.quit(); //driver is not accessible here, as it was defined in Login method. ->  make driver a class level variable to use it in both methods.
            //Additionally, driver should be disposed
            //And, probably it's better to initialize and quit driver in [OneTimeSetUp] and [OneTimeTearDown] if you are going to have more than one test method in this class.
        }
    }
}