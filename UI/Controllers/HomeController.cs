using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Usuario;
using UI.Models;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        public IUsuarioService _service;

        public HomeController(IUsuarioService service)
        {
            _service = service;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult Privacy()
        {
            return View();
        }
        [HttpPost]
        public bool EmailisValid([FromBody] string x)
        {
            var result = _service.EmailIsValid(x);
            return result;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
