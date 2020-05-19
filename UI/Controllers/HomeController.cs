using System.Diagnostics;
using Domain.Interfaces.Application;
using Domain.Interfaces.IPSender;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UI.Models;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        public IUsuarioService _service;
        public IIPService _ipService;
        public HomeController(IUsuarioService service,IIPService iPService)
        {
            _service = service;
            _ipService = iPService;
        }

        [Authorize]
        public IActionResult Index()
        {
            var ips = _ipService.GetAll();
            return View(ips);
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
