using OpenQA.Selenium;

namespace MatchingEngineTests.Pages
{
    public class HomePage : BasePage
    {
        public const string Url = "https://www.matchingengine.com/";

        private By solutionsMenu = By.XPath("//*[normalize-space(text())='Solutions']");

        private By solutionLinks = By.XPath("//a[contains(@href, '/Music-and-copyright-solutions/')]");

        public HomePage(IWebDriver driver) : base(driver) { }

        public void Open()
        {
            driver.Navigate().GoToUrl(Url);
            WaitForPageLoad();
            AcceptCookies();
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, 0);");
        }

        public void ExpandSolutions()
        {
            var menu = wait.Until(d => d.FindElements(solutionsMenu).FirstOrDefault(e => e.Displayed && e.Location.Y < 200));
            ShowStep(menu);

            HoverOver(menu);
            if (GetMenuLinks().Count == 0)
            {
                menu.Click();
            }

            wait.Until(d => GetMenuLinks().Count > 0);
        }

        private List<IWebElement> GetMenuLinks()
        {
            return driver.FindElements(solutionLinks)
                         .Where(e => e.Displayed && e.Location.Y < 900)
                         .ToList();
        }

        private string GetName(IWebElement link)
        {
            return link.Text.Split('\n')[0].Trim();
        }

        public List<string> GetSolutionNames()
        {
            var names = new List<string>();
            foreach (var link in GetMenuLinks())
            {
                ShowStep(link);
                string name = GetName(link);
                if (name != "" && !names.Contains(name))
                {
                    names.Add(name);
                }
            }
            return names;
        }

        public void ClickSolution(string name)
        {
            var link = wait.Until(d => GetMenuLinks().FirstOrDefault(e =>
                GetName(e).Equals(name, StringComparison.OrdinalIgnoreCase)));
            ShowStep(link);
            link.Click();
        }
    }
}
