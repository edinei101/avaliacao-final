using CatalogoFilmesTempo.Data;
using CatalogoFilmesTempo.Interfaces;
using CatalogoFilmesTempo.Models.Api;
using CatalogoFilmesTempo.Repositories;
using CatalogoFilmesTempo.Services;
using CatalogoFilmesTempo.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore.Sqlite; // Garanta que este using exista!

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    
    options.UseSqlite(connectionString));

// --- 2. Configurações de Options (RF05) ---
builder.Services.Configure<TmdbConfiguration>(
    builder.Configuration.GetSection("TmdbConfiguration"));

// --- 3. Registro de Repositórios e Serviços (Injeção de Dependência) ---

// Repositório Local (RF01, RF03)
builder.Services.AddScoped<IFilmeRepository, FilmeRepository>();

// Utilitário de Exportação (RF08)
builder.Services.AddScoped<ExportService>();

// Cache
builder.Services.AddMemoryCache();

// =================================================================
// 3.1. TMDb Services - Solução Decorator (Para evitar circularidade)
// =================================================================

// 1. REGISTRA O SERVIÇO BASE CONCRETO (TmdbApiService) com HttpClient
builder.Services.AddHttpClient<TmdbApiService>(client =>
{
    // Define a URL base para a API TMDb (v3)
    client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
});

// 2. REGISTRA O DECORATOR COMO A IMPLEMENTAÇÃO FINAL DA INTERFACE (ITmdbApiService)
builder.Services.AddScoped<ITmdbApiService, TmdbServiceCacheDecorator>(provider =>
{
    // O Decorator agora injeta o serviço base concreto (TmdbApiService)
    var tmdbApiService = provider.GetRequiredService<TmdbApiService>();
    var cache = provider.GetRequiredService<IMemoryCache>();

    // O Decorator envolve o serviço real
    return new TmdbServiceCacheDecorator(tmdbApiService, cache);
});


// =================================================================
// 3.2. Weather Services - Solução Decorator (Para evitar circularidade)
// =================================================================

// 1. REGISTRA O SERVIÇO BASE CONCRETO (WeatherApiService) com HttpClient
builder.Services.AddHttpClient<WeatherApiService>(client =>
{
    // URL Base para a API de Clima (Exemplo usando OpenWeatherMap)
    client.BaseAddress = new Uri("https://api.openweathermap.org/");
});

// 2. REGISTRA O DECORATOR COMO A IMPLEMENTAÇÃO FINAL DA INTERFACE (IWeatherApiService)
builder.Services.AddScoped<IWeatherApiService, WeatherServiceCacheDecorator>(provider =>
{
    // O Decorator agora injeta o serviço base concreto (WeatherApiService)
    var weatherApiService = provider.GetRequiredService<WeatherApiService>();
    var cache = provider.GetRequiredService<IMemoryCache>();

    // O Decorator envolve o serviço real
    return new WeatherServiceCacheDecorator(weatherApiService, cache);
});

// --- 4. Configuração MVC Padrão ---
builder.Services.AddControllersWithViews();


var app = builder.Build(); // A compilação deve passar agora!

// --- 5. Configuração do Pipeline HTTP ---
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Filmes}/{action=Buscar}/{id?}"); // Inicia na tela de busca

app.Run();