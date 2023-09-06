using BlogDapper.Models;
using BlogDapper.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace BlogDapper.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriasController : Controller
    {
        private readonly ICategoriaRepositorio _repoCategoria;

        public CategoriasController(ICategoriaRepositorio repoCategoria)
        {
            _repoCategoria = repoCategoria;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear([Bind("IdCategoria, Nombre, FechaCreacion")] Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return View(categoria);

            }
            _repoCategoria.CrearCategoria(categoria);
            return RedirectToAction(nameof(Index));
        }

        #region
        [HttpGet]
        public IActionResult GetCategorias()
        {
            return Json(new { data = _repoCategoria.GetCategorias() });
        }

        #endregion
    }
}
