using BlogDapper.Models;
using BlogDapper.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogDapper.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ComentariosController : Controller
    {
        private readonly IComentarioRepositorio _repoComentario;

        public ComentariosController(IComentarioRepositorio repoComentario)
        {
            _repoComentario = repoComentario;
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
        public IActionResult Crear([Bind("IdComentario, Nombre, FechaCreacion")] Comentario comentario)
        {
            if (!ModelState.IsValid)
            {
                return View(comentario);

            }
            _repoComentario.CrearComentario(comentario);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var Comentario = _repoComentario.GetComentario(id.GetValueOrDefault());

            if (Comentario == null)
            {
                return NotFound();
            }

            return View(Comentario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(int id, [Bind("IdComentario, Nombre, FechaCreacion")] Comentario comentario)
        {
            if (id != comentario.IdComentario)
            {
                return NotFound(comentario);
            }

            if (!ModelState.IsValid)
            {
                return View(comentario);

            }
            _repoComentario.ActualizarComentario(comentario);
            return RedirectToAction(nameof(Index));
        }

        #region
        [HttpGet]
        public IActionResult GetComentarios()
        {
            return Json(new { data = _repoComentario.GetComentarioArticulo() });
        }

        [HttpDelete]
        public IActionResult BorrarComentario(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            _repoComentario.BorrarComentario(id.GetValueOrDefault());
            return Json(new { success = true, message = "Comentario borrada correctamente" });



        }
        #endregion
    }
}
