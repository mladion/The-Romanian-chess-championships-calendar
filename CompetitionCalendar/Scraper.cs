using CompetitionCalendar.Configuration;
using HtmlAgilityPack;
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

        public HtmlNode ParseTable (string cssTableClass)
        {
            string content = GetPageContent();
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(content);
            HtmlNode result = htmlDocument.DocumentNode.SelectSingleNode($"//table[@class='{cssTableClass}']");

            return result;
        }

        public string GetPageContent()
        {
            var result = restClient.Execute(new RestRequest(scraperConfig.SourceUrl, Method.Get)).Content;
            return result;
        }
    }
}
