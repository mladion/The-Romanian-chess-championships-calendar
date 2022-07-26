using CompetitionCalendar;
using CompetitionCalendar.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using RestSharp;
using Xunit;

namespace UnitTests
{
    public class ScraperTests
    {
        public readonly Scraper scraper;
        public readonly RestClient restClient;
        private readonly Mock<IOptions<ScraperConfig>> scraperConfig;

        public ScraperTests()
        {
            restClient = Mock.Of<RestClient>();
            scraperConfig = new Mock<IOptions<ScraperConfig>>();
            scraperConfig.Setup(x => x.Value).Returns(() => new ScraperConfig()
            {
                SourceUrl = "https://frsah.ro/",
                CssTableClass = "wptb-preview-table wptb-element-main-table_setting-18262"
            });

            scraper = new Scraper(restClient, scraperConfig.Object);
        }

        [Fact]
        public void GetPageContent_Test()
        {
            var content = scraper.GetPageContent();
            Assert.NotEmpty(content);
            Assert.NotNull(content);
        }

        [Fact]
        public void ParseTable_Test()
        {
            var content = scraper.ParseTable(scraperConfig.Object.Value.CssTableClass);
            Assert.NotNull(content);
        }
    }
}

