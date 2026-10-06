using MatchingEngineTests.Pages;
using NUnit.Framework;

namespace MatchingEngineTests.Tests
{
    public class SolutionsNavigationTests : BaseTest
    {
        private string[] expectedSolutions =
        {
            "Repertoire management",
            "Repertoire and usage matching",
            "Data ingestion and integration",
            "Distribution processing",
            "Member management",
            "Member self service"
        };

        private Dictionary<string, string> expectedCards = new Dictionary<string, string>
        {
            { "Prevent missing payments", "missing payments" },
            { "Comply with international standards", "ISWC" },
            { "Run analytics and queries", "data lakehouse" },
            { "Manage different collection share pictures", "territories and right types" },
            { "Minimise manual intervention", "carve outs and carve ins" },
            { "Tackle fluctuating data volumes", "scales dynamically" }
        };

        [Test]
        public void VerifyAllInOneSectionOnDistributionProcessingPage()
        {
            var homePage = new HomePage(driver);
            homePage.Open();

            homePage.ExpandSolutions();

            var actualSolutions = homePage.GetSolutionNames();
            foreach (string solution in expectedSolutions)
            {
                Assert.That(actualSolutions, Has.Some.EqualTo(solution).IgnoreCase, solution + " is missing");
            }

            homePage.ClickSolution("Distribution processing");
            var distributionPage = new DistributionProcessingPage(driver);
            distributionPage.WaitForPage();
            Assert.That(driver.Url, Does.Contain("Distribution-processing"));
            Assert.That(distributionPage.GetPageHeading(), Is.EqualTo("Distribution processing").IgnoreCase);

            distributionPage.ScrollToAllInOneSection();
            Assert.That(distributionPage.IsOnScreen(distributionPage.GetAllInOneHeading()), Is.True);

            Assert.Multiple(() =>
            {
                foreach (var card in expectedCards)
                {
                    var title = distributionPage.GetCardTitle(card.Key);
                    string description = distributionPage.GetCardDescription(title);

                    Assert.That(distributionPage.IsBelowAllInOneHeading(title), Is.True, card.Key + " is not in the section");
                    Assert.That(description, Does.Contain(card.Value).IgnoreCase, card.Key + " has the wrong description");
                }
            });
        }
    }
}
