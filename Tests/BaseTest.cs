using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MatchingEngineTests.Tests
{
    public class BaseTest
    {
        protected IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            driver = new ChromeDriver(options);
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                string file = Path.Combine(TestContext.CurrentContext.WorkDirectory, "failed_test.png");
                ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(file);
                TestContext.WriteLine("Screenshot saved: " + file);
            }

            driver.Quit();
            driver.Dispose();
        }
    }
}
