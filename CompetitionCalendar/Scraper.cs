using CompetitionCalendar.Configuration;
using CompetitionCalendar.Models;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using RestSharp;

namespace CompetitionCalendar
{
    public class Scraper : IScraper
    {
        private readonly RestClient restClient;
        private readonly ILogger<Scraper> logger;
        private readonly ScraperConfig scraperConfig;

        public Scraper(RestClient restClient, IOptions<ScraperConfig> scraperConfig, ILogger<Scraper> logger)
        {
            this.restClient = restClient;
            this.logger = logger;
            this.scraperConfig = scraperConfig.Value;
        }

        public IEnumerable<Championship> ParseChampionships()
        {
            List<Championship> championships = new();
            var rows = ParseTable(scraperConfig.CssTableClass)?.Descendants("tr");

            foreach (var row in rows)
            {
                try
                {
                    var cells = row.SelectNodes("td")?.Select(node => node.InnerText);

                    if (cells is null)
                        continue;

                    championships.Add(new Championship
                    {
                        Number = Convert.ToInt32(cells.ElementAt(0)),
                        Competition = cells.ElementAt(1).Trim(),
                        Stage = cells.ElementAt(2).Trim(),
                        Chess = cells.ElementAt(3).Trim(),
                        AgeCategory = cells.ElementAt(4).Trim(),
                        StartDate = cells.ElementAt(5).Trim(),
                        EndDate = cells.ElementAt(6).Trim()
                    });
                }
                catch (Exception e)
                {
                    logger.LogError($"Error encountered when scraping page: {e.Message}, {e.StackTrace}");
                }
            }
            return championships;
        }

        public HtmlNode ParseTable(string cssTableClass)
        {
            string content = GetPageContent();
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(content);

            return htmlDocument.DocumentNode.SelectSingleNode($"//table[@class='{cssTableClass}']");
        }

        public string GetPageContent()
        {
            return restClient.Execute(new RestRequest(scraperConfig.SourceUrl, Method.Get)).Content;
        }
    }
}
