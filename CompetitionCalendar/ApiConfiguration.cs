using System;
using CompetitionCalendar.Configuration;
using RestSharp;

namespace CompetitionCalendar
{
    public static class ApiConfiguration
    {
        public static void ConfigureEndpoints(this WebApplication app)
        {
            app.MapGet("/check", () =>
            {
                return Results.Ok(new
                {
                    Message = "Welcome to FRSah Rest API"
                });
            });

            app.MapGet("/championships", (IScraper scraper) =>
            {
                try
                {
                    return Results.Ok(scraper.ParseChampionships());
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });
        }

        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Logging.AddLog4Net();
            builder.Services.Configure<ScraperConfig>(builder.Configuration.GetSection("ScraperConfig"));

            builder.Services.AddSingleton(new RestClient(new HttpClient()));
            builder.Services.AddScoped<IScraper, Scraper>();
        }
    }
}
