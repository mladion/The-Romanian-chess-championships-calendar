using CompetitionCalendar.Configuration;
using RestSharp;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.AddLog4Net();
builder.Services.Configure<ScraperConfig>(builder.Configuration.GetSection("ScraperConfig"));

builder.Services.AddSingleton(new RestClient(new HttpClient()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello World!");

app.Run();
