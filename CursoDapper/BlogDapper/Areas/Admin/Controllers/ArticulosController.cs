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
        private readonly IEtiquetaRepositorio _repoEtiqueta;

        public ArticulosController(IArticuloRepositorio repoArticulo, ICategoriaRepositorio repoCategoria, IWebHostEnvironment hostingEnvironment, IEtiquetaRepositorio repoEtiqueta)
        {
            _repoArticulo = repoArticulo;
            _repoCategoria = repoCategoria;
            _hostingEnvironment = hostingEnvironment;
            _repoEtiqueta = repoEtiqueta;
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

            var articulo = _repoArticulo.GetArticulo(id.GetValueOrDefault());

            if (articulo == null)
            {
                return NotFound();
            }
            ViewBag.SelectList = _repoCategoria.GetListaCategorias();
            return View(articulo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(int id, [Bind("IdArticulo, Titulo, Descripcion, Imagen, Estadoo, CategoriaId, FechaCreacion")] Articulo articulo)
        {
            if (!ModelState.IsValid)
            {
                return View(articulo);

            }

            string rutaPrincial = _hostingEnvironment.WebRootPath;
            var archivos = HttpContext.Request.Form.Files;

            var articuloDesdeDB = _repoArticulo.GetArticulo(id);

            if (archivos.Count() > 0)
            {
                //Editamos o cambiamos la imgen del articulo
                string nombreArchivo = Guid.NewGuid().ToString();
                var subidas = Path.Combine(rutaPrincial, @"imagenes\articulos");
                var extension = Path.GetExtension(archivos[0].FileName);
                var nuevaExtensión = Path.GetExtension(archivos[0].FileName);

                var rutaImagen = Path.Combine(rutaPrincial, articuloDesdeDB.Imagen.TrimStart('\\'));

                if (System.IO.File.Exists(rutaImagen))
                {
                    System.IO.File.Delete(rutaImagen);
                }

                //subirmos el nuevo archivo

                using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + nuevaExtensión), FileMode.Create))
                {
                    archivos[0].CopyTo(fileStreams);
                }

                articulo.Imagen = @"\imagenes\articulos\" + nombreArchivo + nuevaExtensión;
                _repoArticulo.ActualizarArticulo(articulo);
                return RedirectToAction(nameof(Index));

            }
            else
            {
                //aquí es cuan la imagen ya existe pero no se reemplaza
                articulo.Imagen = articuloDesdeDB.Imagen;
                _repoArticulo.ActualizarArticulo(articulo);
                return RedirectToAction(nameof(Index));
            }
            //esta linea valdia el modelo si es "false" retorna a la vista crear pero del get, o sea al formulario
            return RedirectToAction(nameof(Crear));
        }

        //para la parte de asignar etiquetas a un artículo
        [HttpGet]
        public IActionResult AsignarEtiquetas(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var articulo = _repoArticulo.GetArticulo(id.GetValueOrDefault());
            if (articulo is null)
            {
                return NotFound();
            }

            ViewBag.SelectList = _repoEtiqueta.GetListaEtiquetas();

            return View(articulo);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AsignarEtiquetas(int? idArticulo, int? IdEtiqueta)
        {
            if (idArticulo is null || IdEtiqueta is null )
            {
                return NotFound();
            }

            if (idArticulo.GetValueOrDefault() == 0 || IdEtiqueta.GetValueOrDefault() == 0)
            {
                ViewBag.SelectList = _repoEtiqueta.GetListaEtiquetas();
                return View();
            }

            ArticuloEtiquetas artiEtiquetas = new ArticuloEtiquetas();
            artiEtiquetas.IdArticulo = idArticulo.GetValueOrDefault();
            artiEtiquetas.IdEtiqueta = IdEtiqueta.GetValueOrDefault();

            _repoEtiqueta.AsignarEtiquetas(artiEtiquetas);
            return RedirectToAction(nameof(Index));

        }

        #region
        [HttpGet]
        public IActionResult GetArticulos()
        {
            return Json(new { data = _repoArticulo.GetArticuloCategoria() });
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
