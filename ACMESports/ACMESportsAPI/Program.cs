using ACMESportsAPI.Models.RequestResponse;
using ACMESportsAPI.Services;
using Polly.Contrib.WaitAndRetry;
using Polly;
using Serilog.Events;
using Serilog;
using ACMESportsAPI.Models;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Setup Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var section = builder.Configuration.GetSection("ThirdPartyAPI");

var baseUri = section["BaseUrl"] ?? "http://localhost:8000/";
var requestheaders = builder.Configuration["REQUEST_HEADERS"] ?? "X-API-Key";

builder.Services.Configure<ThirdPartyAPISettings>(builder.Configuration.GetSection("ThirdPartyAPI"));
builder.Services.AddHttpClient("ThirdPartyAPI",
    (services, c) =>
    {
        c.BaseAddress = new Uri(baseUri);
        c.DefaultRequestHeaders.Add("X-API-Key", requestheaders);
    })
.AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 5)));

builder.Services.AddSingleton<IEventService, EventService>();
builder.Logging.AddSerilog();
builder.Host.UseSerilog();

var app = builder.Build();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();



app.MapPost("/events", async (EventsRequest eventsRequest, IEventService eventService, IOptions<ThirdPartyAPISettings> settings, ILogger<EventService> logger) =>
{
    logger.LogInformation("Received event request for league: {League}", eventsRequest.League);
    var events = await eventService.GetAggregatedEvents(eventsRequest.League, eventsRequest.StartDate, eventsRequest.EndDate);
    return Results.Ok(events);
}).Produces<EventResponse>(200)
  .Produces(400)
  .Produces(500)
  .WithName("PollingEvents");

app.Run();

Log.CloseAndFlush();