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
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ArticulosController(IArticuloRepositorio repoArticulo, ICategoriaRepositorio repoCategoria, IWebHostEnvironment hostingEnvironment)
        {
            _repoArticulo = repoArticulo;
            _repoCategoria = repoCategoria;
            _hostingEnvironment = hostingEnvironment;
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
        public IActionResult Crear([Bind("IdArticulo, Titulo, Descripcion, Imagen, Estadoo, CategoriaId, FechaCreacion")] Articulo articulo)
        {
            if (!ModelState.IsValid)
            {
                return View(articulo);

            }

            string rutaPrincial = _hostingEnvironment.WebRootPath;
            var archivos = HttpContext.Request.Form.Files;

            if (articulo.IdArticulo == 0)
            {
                string nombreArchivo = Guid.NewGuid().ToString();
                var subidas = Path.Combine(rutaPrincial, @"imagenes\articulos");
                var extension = Path.GetExtension(archivos[0].FileName);

                using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                {
                    archivos[0].CopyTo(fileStreams);
                }

                articulo.Imagen = @"\imagenes\articulos\" + nombreArchivo + extension;
                _repoArticulo.CrearArticulo(articulo);
                return RedirectToAction(nameof(Index));

            }
            //esta linea valdia el modelo si es "false" retorna a la vista crear pero del get, o sea al formulario
            return RedirectToAction(nameof(Crear));
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
