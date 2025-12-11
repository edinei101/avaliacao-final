// Program.cs

using Microsoft.EntityFrameworkCore;
using CatalogoFilmesTempo.Data;
using CatalogoFilmesTempo.Repositories;
using CatalogoFilmesTempo.Services;
using CatalogoFilmesTempo.Models.Api;
using CatalogoFilmesTempo.Interfaces;
using System;
using Microsoft.Extensions.DependencyInjection; // Necessário para GetRequiredService

var builder = WebApplication.CreateBuilder(args);

// 1. Configuração do Banco de Dados (AppDbContext)
// Usando SQLite, conforme o appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Configuração de Opções (TmdbConfiguration)
// CORREÇÃO CRUCIAL: Mapeia a seção "TmdbOptions" do appsettings.json
builder.Services.Configure<TmdbConfiguration>(
    builder.Configuration.GetSection(TmdbConfiguration.Tmdb));

// 3. Registro de Repositórios e Serviços
// Repositórios
builder.Services.AddScoped<IFilmeRepository, FilmeRepository>();

// Serviços da API TMDb
builder.Services.AddHttpClient<ITmdbApiService, TmdbApiService>(client =>
{
    // Obtém a URL base da configuração
    var tmdbConfig = builder.Configuration.GetSection(TmdbConfiguration.Tmdb).Get<TmdbConfiguration>();
    if (tmdbConfig != null && !string.IsNullOrEmpty(tmdbConfig.BaseUrl))
    {
        client.BaseAddress = new Uri(tmdbConfig.BaseUrl);
    }
});

// Serviços de Clima (Com Decorator para Cache)
// Registra o serviço concreto que usa HttpClient
builder.Services.AddHttpClient<WeatherApiService>(client =>
{
    // Exemplo: URL Base da Open-Meteo
    var weatherConfig = builder.Configuration.GetSection("WeatherApiConfiguration");
    client.BaseAddress = new Uri(weatherConfig["BaseUrl"] ?? "https://api.open-meteo.com/v1/");
});

// Registra o Decorator (WeatherServiceCacheDecorator) para a interface (IWeatherApiService)
builder.Services.AddScoped<IWeatherApiService, WeatherServiceCacheDecorator>(provider =>
{
    var realService = provider.GetRequiredService<WeatherApiService>();
    // Cria e retorna o Decorator
    return new WeatherServiceCacheDecorator(realService);
});

// 4. Configuração MVC e Serviços Padrão
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 5. Pipeline de Requisições (Middleware)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
// Cria o DB se não existir (Opcional, mas útil para SQLite)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// 6. Mapeamento de Controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();