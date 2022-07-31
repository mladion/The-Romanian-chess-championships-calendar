using CompetitionCalendar;
using CompetitionCalendar.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using RestSharp;
using Xunit;

namespace UnitTests
{
    public class ScraperTests
    {
        private readonly Scraper scraper;
        private readonly Mock<RestClient> restClient;
        private readonly Mock<ILogger<Scraper>> logger;
        private readonly Mock<IOptions<ScraperConfig>> scraperConfig;

        public ScraperTests()
        {
            restClient = new Mock<RestClient>();
            logger = new Mock<ILogger<Scraper>>();
            scraperConfig = new Mock<IOptions<ScraperConfig>>();
            scraperConfig.Setup(x => x.Value).Returns(() => new ScraperConfig()
            {
                SourceUrl = "https://frsah.ro/",
                CssTableClass = "wptb-preview-table wptb-element-main-table_setting-18262"
            });

            scraper = new Scraper(restClient.Object, scraperConfig.Object, logger.Object);
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
