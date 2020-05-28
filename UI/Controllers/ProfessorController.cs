using CrossCutting.Model;
using Domain.Interfaces.Application;
using Domain.Interfaces.IPSender;
using Infra.Repository.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.ModelView;
using UI.ViewModels;

namespace UI.Controllers
{
    public class ProfessorController : Controller
    {
        public IUsuarioService _service;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ProfessorController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private IEnumerable<ApplicationUser> _alunos;
        public IProfessorRepository _repProfessor;
        public IIPService _ipService;
        public ProfessorController(IUsuarioService service, UserManager<ApplicationUser> userManager,
            ILogger<ProfessorController> logger, RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IProfessorRepository repo,
            IIPService _ip)
        {
            _service = service;
            _userManager = userManager;
            _logger = logger;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _alunos = new List<ApplicationUser>();
            _repProfessor = repo;
            _ipService = _ip;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Registro()
        {
            return View();
        }
        [HttpGet, ActionName("Gerenciamento")]
        public IActionResult Gerenciamento(string id)
        {
            var alunos = _repProfessor.GetAlunos(id);
            return View(alunos);
        }

        [HttpPost, ActionName("Login")]
        public async Task<IActionResult> LoginPost(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
               
                var user = await _signInManager.UserManager.FindByEmailAsync(loginModel.Email);
                var roles = await _signInManager.UserManager.GetRolesAsync(user);
                
                var EhProfessor = roles.Where(x => x.Equals("Professor")).Any();
                if (EhProfessor)
                { 
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, loginModel.Password, loginModel.RememberMe, false);

                    if (result.Succeeded)
                        return RedirectToAction("Gerenciamento", "Professor", new { id = user.Id });
                }

            }
            ModelState.AddModelError("", "Usuário não é professor , tente no usuário jogador.");
            return RedirectToAction("Index", "Professor");
        }

        public async Task<IActionResult> RegistroProfessor([FromForm]CadastroProfessorModelView cadastroProfessorModelView)
        {
            if (cadastroProfessorModelView != null)
            {
                if (!string.IsNullOrEmpty(cadastroProfessorModelView.Email)
                && !string.IsNullOrEmpty(cadastroProfessorModelView.Password)
                && !string.IsNullOrEmpty(cadastroProfessorModelView.RoleName))
                {
                    var user = new ApplicationUser { UserName = cadastroProfessorModelView.UserName, Email = cadastroProfessorModelView.Email ,RoleName =cadastroProfessorModelView.RoleName};
                    var result = await _userManager.CreateAsync(user, cadastroProfessorModelView.Password);

                    if (result.Succeeded)
                    {
                        bool roleExists = await _roleManager.RoleExistsAsync(cadastroProfessorModelView.RoleName);
                        if (!roleExists)
                        {
                            await _roleManager.CreateAsync(new IdentityRole(cadastroProfessorModelView.RoleName));
                            var permissao = await _userManager.AddToRoleAsync(user, "Professor");
                            if (permissao.Succeeded)
                            {
                                
                                _logger.LogInformation("User created a new teacher account with password.");
                                return Redirect("/Professor/Index");
                            }
                        }
                        if (!await _userManager.IsInRoleAsync(user, cadastroProfessorModelView.RoleName))
                        {
                            await _userManager.AddToRoleAsync(user, cadastroProfessorModelView.RoleName);
                            return RedirectToAction("Index", "Professor");
                        }
                     
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                return RedirectToPage("/Professor/Index");

            }
            return RedirectToPage("/Professor/Index");
        }

    }
}