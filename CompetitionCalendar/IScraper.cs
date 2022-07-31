using CompetitionCalendar.Models;
using HtmlAgilityPack;

namespace CompetitionCalendar
{
    public interface IScraper
    {
        string GetPageContent();
        IEnumerable<Championship> ParseChampionships();
        HtmlNode ParseTable(string cssTableClass);
    }
}