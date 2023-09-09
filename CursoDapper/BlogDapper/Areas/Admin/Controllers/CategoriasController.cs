using BlogDapper.Models;
using BlogDapper.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogDapper.Areas.Admin.Controllers
{
    [Authorize]
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
        [ValidateAntiForgeryToken]
        public IActionResult Crear([Bind("IdCategoria, Nombre, FechaCreacion")] Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return View(categoria);

            }
            _repoCategoria.CrearCategoria(categoria);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var categoria = _repoCategoria.GetCategoria(id.GetValueOrDefault());

            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(int id, [Bind("IdCategoria, Nombre, FechaCreacion")] Categoria categoria)
        {
            if (id != categoria.IdCategoria)
            {
                return NotFound(categoria);
            }

            if (!ModelState.IsValid)
            {
                return View(categoria);

            }
            _repoCategoria.ActualizarCategoria(categoria);
            return RedirectToAction(nameof(Index));
        }

        #region
        [HttpGet]
        public IActionResult GetCategorias()
        {
            return Json(new { data = _repoCategoria.GetCategorias() });
        }

        [HttpDelete]
        public IActionResult BorrarCategoria(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            _repoCategoria.BorrarCategoria(id.GetValueOrDefault());
            return Json(new { success = true, message = "Categoría borrada correctamente" });



        }
        #endregion
    }
}
