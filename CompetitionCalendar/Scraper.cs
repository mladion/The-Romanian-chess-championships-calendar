using CompetitionCalendar.Configuration;
using Microsoft.Extensions.Options;
using RestSharp;

namespace CompetitionCalendar
{
    public class Scraper
    {
        private readonly RestClient restClient;
        private readonly ScraperConfig scraperConfig;

        public Scraper(RestClient restClient, IOptions<ScraperConfig> scraperConfig)
        {
            this.restClient = restClient;
            this.scraperConfig = scraperConfig.Value;
        }

        public string GetPageContent()
        {
            return restClient.Execute(new RestRequest(scraperConfig.SourceUrl, Method.Get)).Content;
        }
    }
}
