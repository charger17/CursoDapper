﻿using BlogDapper.Models;
using BlogDapper.Repositorio;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogDapper.Areas.Front.Controllers
{
    [Authorize]
    [Area("Front")]
    public class AccesosController : Controller
    {
        private readonly IAccesoRepositorio _repoAcceso;

        public AccesosController(IAccesoRepositorio repoAcceso)
        {
            _repoAcceso = repoAcceso;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Acceso()
        {
            return View();
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Registro(Usuario user)
        {
            if (!ModelState.IsValid)
            {
                TempData["mensajeConfirmacion"] = "Algunos campos obligatorios están vacios";
                return View(user);
            }

            var existeUser = _repoAcceso.ValidateUser(user);

            if (existeUser)
            {
                TempData["mensajeConfirmacion"] = "El usuario ya Existe";
                return View(user);
            }

            _repoAcceso.AddUser(user);

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Login)
                };

            var claimsIdentity = new ClaimsIdentity(claims, "Login");

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return RedirectToAction("Index", "Inicio");

        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Salir()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Inicio");
        }
    }
}
