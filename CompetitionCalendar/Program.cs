using CompetitionCalendar;
using CompetitionCalendar.Configuration;
using RestSharp;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureEndpoints();

app.Run();
