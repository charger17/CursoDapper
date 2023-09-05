using AgendaDapper.Models;
using AgendaDapper.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AgendaDapper.Controllers
{
    public class InicioController : Controller
    {
        private readonly ILogger<InicioController> _logger;
        private readonly IRepositorio _repoo;

        public InicioController(ILogger<InicioController> logger, IRepositorio repo)
        {
            _logger = logger;
            _repoo = repo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_repoo.GetClientes());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}