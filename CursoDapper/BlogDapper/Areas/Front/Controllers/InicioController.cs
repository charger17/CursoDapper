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

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["ListaSlider"] = _repoSlider.GetSliders();

            var articulos = _repoSlider.GetArticulosForSlider();
            ViewData["ListaCategorias"] = _repoSlider.GetCategoriasForSlider();

            //Esta linea es para poder saber si estamos en el home o no
            ViewBag.isHome = true;
            return View(articulos);
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

        [HttpGet]
        public IActionResult Detalle(int? id)
        {
            if (id is null)
            {
                return RedirectToAction(nameof(Index));
            }
            var articulo = _repoSlider.GetArticuloForSlider(id.GetValueOrDefault());
            ViewData["ListaComentarios"] = _repoSlider.GetComentariosForSlider(id.GetValueOrDefault());

            return View(articulo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Detalle(string Titulo, string Mensaje, int ArticuloId)
        {
            
             _repoSlider.InsertComentarioForSlider(new Comentario
             {
                 Titulo = Titulo,
                 Mensaje = Mensaje,
                 ArticuloId = ArticuloId
             });

            return RedirectToAction(nameof(Index));
        }
    }
}