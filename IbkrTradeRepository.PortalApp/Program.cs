using IbkrTradeRepository.PortalApp.Data;
using IbkrTradeRepository.PortalApp.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddDbContext<PortfolioDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("PortfolioDb") ?? throw new ArgumentException("Empty connection string");
    options.UseNpgsql(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

if (app.Environment.IsDevelopment())
{
    try
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<PortfolioDbContext>();
            dbContext.Database.Migrate(); // Applies migrations automatically
            Console.WriteLine("Development database migrations applied successfully.");
        }
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine($"An error occurred while migrating the database: {ex.Message}");
    }
}

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
