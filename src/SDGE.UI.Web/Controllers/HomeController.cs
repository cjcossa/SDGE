using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.UI.Web.Models;

namespace SDGE.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEventoRepository _eventoRepository;


        public HomeController(ILogger<HomeController> logger,
            IEventoRepository eventoRepository)
        {
            _logger = logger;
            _eventoRepository = eventoRepository;
        }

        public IActionResult Index()
        {
            return View(_eventoRepository.ObterTodos());
        }
        public IActionResult PIndex()
        {
            return View();
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
    }
}
