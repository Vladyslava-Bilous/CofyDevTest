using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestTheTest
{
    public class FixedLoginAutomation
    {
        private IWebDriver _driver; 

        [SetUp]
        public void SetUp()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
        }

        [Test]
        public void Login()
        {
            //Arrange
            const string userNameValue = "john_smith@sorted.com"; 
            const string passwordValue = "Pa55w0rd!\"\"";
            const string expectedUrl = "http://qa.sorted.com/newtrack/loginSuccess"; 

            //Act
            _driver.Navigate().GoToUrl("https://qa.sorted.com/newtrack");
            var userNameInput = _driver.FindElement(By.XPath("//form[@id='loginForm']/input[1]"));
            var passwordInput = _driver.FindElement(By.XPath("//form[@id='loginForm']/input[2]"));
            var loginButton = _driver.FindElement(By.XPath("//submit"));
            userNameInput.SendKeys(userNameValue);
            passwordInput.SendKeys(passwordValue);
            loginButton.Click();
            var actualUrl = _driver.Url;

            //Assert
            Assert.That(actualUrl, Is.EqualTo(expectedUrl));

        }

        [TearDown]
        public void Teardown()
        {
            _driver.Quit(); 
            _driver.Dispose();
        }
    }
}