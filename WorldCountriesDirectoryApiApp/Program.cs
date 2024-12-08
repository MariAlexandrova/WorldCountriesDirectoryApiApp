using WorldCountriesDirectoryApiApp.Model.Scenarious;
using WorldCountriesDirectoryApiApp.Model.Service;
using WorldCountriesDirectoryApiApp.Storage;

var builder = WebApplication.CreateBuilder(args);

// ����������� ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>();

builder.Services.AddTransient<CountryScenarios>();
// ����������� CountryStorage � ICountryStorage
builder.Services.AddTransient<ICountryStorage, CountryStorage>();

builder.Services.AddControllers();

var app = builder.Build();

// ����������� ��������� ������������
app.MapControllers();

app.Run();
