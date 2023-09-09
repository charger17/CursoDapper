using BlogDapper.Models;
using BlogDapper.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogDapper.Areas.Front.Controllers
{
    [Area("Front")]
    public class InicioController : Controller
    {
        private readonly ILogger<InicioController> _logger;
        private readonly ISliderRepository _repoSlider;

        public InicioController(ILogger<InicioController> logger, ISliderRepository repoSlider)
        {
            _logger = logger;
            _repoSlider = repoSlider;
        }

        public IActionResult Index()
        {
            ViewData["ListaCategorias"] = _repoSlider.GetSliders();

            //Esta linea es para poder saber si estamos en el home o no
            ViewBag.isHome = true;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}