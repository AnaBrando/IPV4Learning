using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using CrossCutting.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Domain.Interfaces.IPSender;
using Infra.Repository.Helper;
using System.Collections.Generic;

namespace UI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [Display(Name = "Login Name")]
        public string UserName { get; set; }
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IIPService IpService;
        private readonly IProfessorRepository _repo;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IIPService _ipService,
            RoleManager<IdentityRole> roleManager,
            IProfessorRepository repo
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            IpService = _ipService;
            _roleManager = roleManager;
            _repo = repo;
        }

        [BindProperty]
        [FromBody]
        public InputModel Input { get; set; }

        public string RoleName { get; set; }

        public string ProfessorID { get; set; }
        public string ReturnUrl { get; set; }

        public List<ApplicationUser> _professores;
        public class InputModel
        {

            [Required]
            [Display(Name = "UserName")]
            public string UserName { get; set; }

     
            [Required]
            [Display(Name = "RoleName")]
            public string RoleName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "ProfessorID")]
            public string ProfessorID { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            var professores = _repo.GetProfessores();
            _professores = professores;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("CheckEmail");
            if (ModelState.IsValid && Input.ProfessorID != null)
            {
                
                var user = new ApplicationUser { UserName = Input.UserName, Email = Input.Email ,ProfessorId = Input.ProfessorID, RoleName= Input.RoleName};
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    bool roleExists = await _roleManager.RoleExistsAsync(Input.RoleName);
                    if (!roleExists)
                    {
                        await _roleManager.CreateAsync(new IdentityRole(Input.RoleName));
                        var permissao = await _userManager.AddToRoleAsync(user, Input.RoleName);

                        if (permissao.Succeeded)
                        {
                            _logger.LogInformation("User created a new teacher account with password.");
                            _logger.LogInformation("User created a new account with password.");

                            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                            var callbackUrl = Url.Page(
                                "/Account/ConfirmEmail",
                                pageHandler: null,
                                values: new { userId = user.Id, code = code },
                                protocol: Request.Scheme);
                            var x = IpService.GetIP();
                            await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                                $"Seu Ip de Acesso é : {x.ip}");

                            //return RedirectToPage("./CheckEmail", user);
                            //return new JsonResult(returnUrl);
                            return new JsonResult(new
                            {
                                redirectUrl = Url.Page("CheckEmail", "OnGet", user),
                                isRedirect = true

                            });
                        }
                    }

                    if (!await _userManager.IsInRoleAsync(user, Input.RoleName))
                    {
                        var permissao = await _userManager.AddToRoleAsync(user, Input.RoleName);
                        if (permissao.Succeeded)
                        {
                            _logger.LogInformation("User created a new teacher account with password.");
                            _logger.LogInformation("User created a new account with password.");

                            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                            var callbackUrl = Url.Page(
                                "/Account/ConfirmEmail",
                                pageHandler: null,
                                values: new { userId = user.Id, code = code },
                                protocol: Request.Scheme);
                            var x = IpService.GetIP();
                            await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                                $"Seu Ip de Acesso é : {x.ip}");

                            //return RedirectToPage("./CheckEmail", user);
                            //return new JsonResult(returnUrl);
                            return new JsonResult(new
                            {
                                redirectUrl = Url.Page("CheckEmail", "OnGet", user),
                                isRedirect = true

                            });
                        }
                    }





                }

            }
            return new JsonResult(returnUrl);
        }
    }
}