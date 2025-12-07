// Controllers/HomeController.cs
using CatalogoFilmesTempo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CatalogoFilmesTempo.Controllers
{
    // O HomeController é o controlador padrão, frequentemente usado para páginas estáticas (Home, Privacy).
    public class HomeController : Controller
    {
        // GET: /Home/Index
        public IActionResult Index()
        {
            // Redirecionamos para a nossa rota principal de catálogo
            return RedirectToAction("Catalogo", "Filmes");
        }

        // GET: /Home/Privacy
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // GET: /Home/Error
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}