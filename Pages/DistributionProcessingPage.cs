using OpenQA.Selenium;

namespace MatchingEngineTests.Pages
{
    public class DistributionProcessingPage : BasePage
    {
        private By pageHeading = By.XPath("//h1 | //h2[normalize-space()='Distribution processing']");
        private By allInOneHeading = By.XPath("//h2[normalize-space()='All-in-one solution for scale']");

        public DistributionProcessingPage(IWebDriver driver) : base(driver) { }

        public void WaitForPage()
        {
            wait.Until(d => d.Url.Contains("Distribution-processing"));
            WaitForPageLoad();
            WaitForVisible(allInOneHeading);
        }

        public string GetPageHeading()
        {
            return WaitForVisible(pageHeading).Text.Trim();
        }

        public IWebElement GetAllInOneHeading()
        {
            return WaitForVisible(allInOneHeading);
        }

        public void ScrollToAllInOneSection()
        {
            var heading = GetAllInOneHeading();
            ScrollTo(heading);
            ShowStep(heading);
        }

        public IWebElement GetCardTitle(string title)
        {
            var card = WaitForVisible(By.XPath("//h5[normalize-space()='" + title + "']"));
            ScrollTo(card);
            ShowStep(card);
            return card;
        }

        public string GetCardDescription(IWebElement cardTitle)
        {
            var next = cardTitle.FindElements(By.XPath("following-sibling::*[normalize-space()][1]"));
            if (next.Count == 0)
            {
                next = cardTitle.FindElements(By.XPath("./parent::*/following-sibling::*[normalize-space()][1]"));
            }
            return next.Count > 0 ? next[0].Text.Trim() : "";
        }

        public bool IsBelowAllInOneHeading(IWebElement element)
        {
            return element.Location.Y > GetAllInOneHeading().Location.Y;
        }
    }
}
