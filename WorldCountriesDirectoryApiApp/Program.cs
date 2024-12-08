using WorldCountriesDirectoryApiApp.Model.Scenarious;
using WorldCountriesDirectoryApiApp.Model.Service;
using WorldCountriesDirectoryApiApp.Storage;

var builder = WebApplication.CreateBuilder(args);

// Регистрация ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>();

builder.Services.AddTransient<CountryScenarios>();
// Регистрация CountryStorage и ICountryStorage
builder.Services.AddTransient<ICountryStorage, CountryStorage>();

builder.Services.AddControllers();

var app = builder.Build();

// Подключение маршрутов контроллеров
app.MapControllers();

app.Run();
