// Program.cs

using CatalogoFilmesTempo.Data;
using CatalogoFilmesTempo.Models.Api;
using CatalogoFilmesTempo.Repositories;
using CatalogoFilmesTempo.Services;
using CatalogoFilmesTempo.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using CatalogoFilmesTempo.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Configuração do Banco de Dados (SQLite) ---
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// --- 2. Adicionar Serviços do Projeto ---
builder.Services.AddScoped<IFilmeRepository, FilmeRepository>();
builder.Services.AddScoped<ExportService>();

// --- 3. Configuração de APIs Externas ---
// Configuração TMDb
builder.Services.Configure<TmdbConfiguration>(builder.Configuration.GetSection("TmdbSettings"));
builder.Services.AddHttpClient<TmdbApiService>();
// Serviço com Decorator de Cache (RF08)
builder.Services.AddScoped<ITmdbApiService, TmdbApiService>(); // Serviço base
builder.Services.AddScoped<ITmdbApiService, TmdbServiceCacheDecorator>(); // Decorator (sobrescreve o anterior)

// Configuração Weather (OpenWeatherMap)
builder.Services.AddHttpClient<WeatherApiService>();
builder.Services.AddScoped<IWeatherApiService, WeatherApiService>();

// --- 4. Configuração de Cache em Memória (RF08) ---
builder.Services.AddMemoryCache();

// --- 5. Configuração Padrão do MVC ---
builder.Services.AddControllersWithViews();


var app = builder.Build();

// --- Aplicar Migrações APÓS Build (Melhor Prática) ---
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // Esta linha aplica a migração 'InitialCreate' ao banco de dados.
    dbContext.Database.Migrate();
}

// --- Configuração do Pipeline HTTP ---
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Rota Padrão (Redireciona para o Catálogo)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Filmes}/{action=Catalogo}/{id?}");

app.Run(); ;