using ACMESportsAPI.Models.RequestResponse;
using ACMESportsAPI.Services;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();
builder.Services.AddHttpClient("ThirdPartyAPI", c =>
{
    c.BaseAddress = new Uri("http://localhost:8000");
    c.DefaultRequestHeaders.Add("X-API-Key", "Your-API-Key");
});

builder.Services.AddSingleton<IEventService, EventService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapPost("/events", async(EventsRequest eventsRequest, IEventService eventService) =>
{
    var events = await eventService.GetAggregatedEvents(eventsRequest.League, eventsRequest.StartDate, eventsRequest.EndDate);
   
    return Results.Ok(events);
}).Produces<EventResponse>(200)
  .Produces(400)
  .Produces(500)
  .WithName("PollingEvents");

app.Run();

