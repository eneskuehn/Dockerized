using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Dockers
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture("chrome")]
    [TestFixture("edge")]
    [TestFixture("firefox")]
    public class ParallelTests
    {
        private RemoteWebDriver driver;
        private string browserName;

        public ParallelTests(string browser)
        {
            browserName = browser;
        }

        [SetUp]
        public void Setup()
        {
            dynamic options = GetRemoteBrowserOptions();
            options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");

            string hubUrl = "http://selenium-hub:4444";
            driver = new RemoteWebDriver(new Uri(hubUrl), options.ToCapabilities());
            driver.Navigate().GoToUrl("https://automationintesting.online/");

        }

        [Test]
        public void Test1()
        {
            driver.FindElement(By.Id("name"))
                .SendKeys("Automation Tester");

            driver.FindElement(By.Id("email"))
                .SendKeys("Automation@Tester.Com");

            driver.FindElement(By.Id("phone"))
                .SendKeys("123456789012345");

            driver.FindElement(By.Id("subject"))
                .SendKeys("Hello World!");

            driver.FindElement(By.Id("description"))
                .SendKeys("Hello, this is an automated message!");

            driver.FindElement(By.Id("submitContact"))
                .Click();

            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(ExpectedConditions.ElementIsVisible(By.XPath("//p[text()=\"We'll get back to you about\"]")));
        }

        [TearDown]
        public void Teardown()
        {
            driver.Dispose();
        }

        private dynamic GetRemoteBrowserOptions()
        {
            switch (browserName.ToLower())
            {
                case "chrome":
                    return new ChromeOptions();
                case "edge":
                    return new EdgeOptions();
                case "firefox":
                    return new FirefoxOptions();
                default:
                    ChromeOptions options = new();
                    return options;
            }
        }
    }
}