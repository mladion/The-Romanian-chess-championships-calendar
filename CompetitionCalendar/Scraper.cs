using CompetitionCalendar.Configuration;
using Microsoft.Extensions.Options;
using RestSharp;

namespace CompetitionCalendar
{
	public class Scraper
	{
		private readonly RestClient restClient;
        private readonly IOptions<ScraperConfig> scraperConfig;

        public Scraper(RestClient restClient, IOptions<ScraperConfig> scraperConfig)
        {
			this.restClient = restClient;
            this.scraperConfig = scraperConfig;
        }

        public string GetPageContent()
        {
            return restClient.Execute(new RestRequest(scraperConfig.Value.SourceUrl, Method.Get)).Content;
        }
    }
}
