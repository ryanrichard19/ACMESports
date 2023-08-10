using ACMESportsAPI.Models.RequestResponse;
using ACMESportsAPI.Services;
using Polly.Contrib.WaitAndRetry;
using Polly;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient("ThirdPartyAPI",
    (services, c) =>
    {
        var config = services.GetRequiredService<IConfiguration>();
        c.BaseAddress = new Uri(config["ThirdPartyAPI:BaseUrl"]);
        c.DefaultRequestHeaders.Add("X-API-Key", config["ThirdPartyAPI:ApiKey"]);
    })
.AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 5)));

builder.Services.AddSingleton<IEventService, EventService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapPost("/events", async (EventsRequest eventsRequest, IEventService eventService) =>
{
    var events = await eventService.GetAggregatedEvents(eventsRequest.League, eventsRequest.StartDate, eventsRequest.EndDate);

    return Results.Ok(events);
}).Produces<EventResponse>(200)
  .Produces(400)
  .Produces(500)
  .WithName("PollingEvents");

app.Run();

