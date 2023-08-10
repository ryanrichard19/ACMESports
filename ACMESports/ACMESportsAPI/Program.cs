using ACMESportsAPI.Model;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapPost("/events", (EventsRequest eventsRequest) =>
{
    // Dummy data
    var exampleEvents = new List<Event>
    {
        new Event
        {
            EventId = Guid.NewGuid(),
            EventDateimestamp = DateTime.Now,
            EventTime = "15:30",
            HomeTeamId = Guid.NewGuid(),
            HomeTeamNickName = "Hawks",
            HomeTeamCity = "Hawkstown",
            HomeTeamRank = 1,
            HomeTeamRankPoints = 87.5f,
            AwayTeamId = Guid.NewGuid(),
            AwayTeamNickName = "Eagles",
            AwayTeamCity = "Eagleland",
            AwayTeamRank = 2,
            AwayTeamRankPoints = 100f
        }
    };

    return Results.Ok(exampleEvents);
}).Produces<List<Event>>(200)
  .Produces(400)
  .Produces(500)
  .WithName("PollingEvents");

app.Run();

