using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace MatchingEngineTests.Pages
{
    public class BasePage
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;

        private int slowMo = int.TryParse(Environment.GetEnvironmentVariable("SLOW_MO"), out int ms) ? ms : 0;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));
        }

        protected IWebElement WaitForVisible(By locator)
        {
            return wait.Until(d => d.FindElements(locator).FirstOrDefault(e => e.Displayed));
        }

        protected void WaitForPageLoad()
        {
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").ToString() == "complete");
        }

        public void ScrollTo(IWebElement element)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", element);
            wait.Until(d => IsOnScreen(element));
        }

        public bool IsOnScreen(IWebElement element)
        {
            var result = ((IJavaScriptExecutor)driver).ExecuteScript(
                "var r = arguments[0].getBoundingClientRect();" +
                "return r.top >= 0 && r.bottom <= window.innerHeight;", element);
            return (bool)result;
        }

        protected void HoverOver(IWebElement element)
        {
            new Actions(driver).MoveToElement(element).Perform();
        }

        protected void AcceptCookies()
        {
            try
            {
                var shortWait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                var button = shortWait.Until(d => d.FindElements(By.XPath("//button[contains(., 'Accept')]"))
                                                   .FirstOrDefault(e => e.Displayed));
                button.Click();
            }
            catch (WebDriverTimeoutException)
            {
            }
        }

        protected void ShowStep(IWebElement element)
        {
            if (slowMo == 0) return;
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].style.outline = '3px solid red';", element);
            Thread.Sleep(slowMo);
        }
    }
}
