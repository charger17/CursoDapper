using BlogDapper.Models;
using BlogDapper.Repositorio;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Security.Claims;

namespace BlogDapper.Areas.Front.Controllers
{
    [Area("Front")]
    public class AccesosController : Controller
    {
        private readonly IAccesoRepositorio _repoAcceso;

        public AccesosController(IAccesoRepositorio repoAcceso)
        {
            _repoAcceso = repoAcceso;
        }

        [HttpGet]
        public IActionResult Acceso()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Acceso(Usuario user)
        {
            if (!ModelState.IsValid)
            {
                TempData["mensajeConfirmacion"] = "Algunos campos obligatorios están vacios";
                return View(user);
            }

            var validar = _repoAcceso.UserLogin(user);

            if (validar.Count() == 1)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Login)
                };

                var claimsIdentity = new ClaimsIdentity(claims, "Login");

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("Index", "Inicio");
            }
            else
            {
                TempData["mensajeConfirmacion"] = "Datos de acceso incorrectos";
                return RedirectToAction("Acceso", "Accesos");
            }

        }
    }
}
