using BlogDapper.Models;
using BlogDapper.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace BlogDapper.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticulosController : Controller
    {
        private readonly IArticuloRepositorio _repoArticulo;
        private readonly ICategoriaRepositorio _repoCategoria;

        public ArticulosController(IArticuloRepositorio repoArticulo, ICategoriaRepositorio repoCategoria)
        {
            _repoArticulo = repoArticulo;
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
            ViewBag.SelectList = _repoCategoria.GetListaCategorias();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear([Bind("IdArticulo, Nombre, FechaCreacion")] Articulo articulo)
        {
            if (!ModelState.IsValid)
            {
                return View(articulo);

            }
            _repoArticulo.CrearArticulo(articulo);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var Articulo = _repoArticulo.GetArticulo(id.GetValueOrDefault());

            if (Articulo == null)
            {
                return NotFound();
            }

            return View(Articulo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(int id, [Bind("IdArticulo, Nombre, FechaCreacion")] Articulo articulo)
        {
            if (id != articulo.IdArticulo)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(articulo);

            }
            _repoArticulo.ActualizarArticulo(articulo);
            return RedirectToAction(nameof(Index));
        }

        #region
        [HttpGet]
        public IActionResult GetArticulos()
        {
            return Json(new { data = _repoArticulo.GetArticulos() });
        }

        [HttpDelete]
        public IActionResult BorrarArticulo(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            _repoArticulo.BorrarArticulo(id.GetValueOrDefault());
            return Json(new { success = true, message = "Articulo borrado correctamente" });



        }
        #endregion
    }
}
