using ChessAcademy.Components;
using ChessAcademy.Data;
using ChessAcademy.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add DbContextFactory for Blazor Server (prevents concurrency issues)
builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlite("Data Source=chess_academy.db"));

var app = builder.Build();

// --- SEED DATABASE WITH REALISTIC DATA ---
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated(); // Creates DB if it doesn't exist

    if (!db.Games.Any())
    {
        var players = new[] { "Magnus C.", "Hikaru N.", "Fabiano C.", "Ian N." };
        var openings = new[] { "Sicilian Defense", "Queen's Gambit", "Ruy Lopez", "Caro-Kann", "King's Indian" };
        var nationalities = new[] { "NOR", "USA", "FRA", "RUS", "IND", "CHN" };
        var results = new[] { "Win", "Draw", "Loss" };
        var rng = new Random(42);

        for (int i = 0; i < 40; i++)
        {
            db.Games.Add(new Game
            {
                PlayerName = players[rng.Next(players.Length)],
                MatchDate = DateTime.Now.AddDays(-rng.Next(1, 180)),
                OpponentName = $"Opponent {rng.Next(1, 50)}",
                OpponentNationality = nationalities[rng.Next(nationalities.Length)],
                Result = results[rng.Next(results.Length)],
                Opening = openings[rng.Next(openings.Length)]
            });
        }
        db.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();