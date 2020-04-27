using CrossCutting.Model;
using Domain.Interfaces.Application;
using Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace UI.Areas.Identity.Pages.Account
{

    public class CheckModel
    {

        [Display(Name = "Picture:")]
        [Required]
        public IFormFile PhotoAvatar { get; set; }

        public string ImageName { get; set; }

        public byte[] PhotoFile { get; set; }

        public string ImageMimeType { get; set; }

    }

    public class CheckEmail : PageModel
    {
        private readonly IUsuarioService _service;

        [BindProperty]
        public CheckModel Input { get; set; }

        [BindProperty]
        public string IP { get; set; }

        public CheckEmail(IUsuarioService service)
        {
            this._service = service;
        }


        public string Saudacao { get; set; }

        private Usuario _user { get; set; }

        public void OnGet(Usuario user)       
        {
            if (string.IsNullOrEmpty(user.Id))
            {
                RedirectToPage("./Home");
            }
            else
            {
                Saudacao = "VAMOS APRENDER NESSA PORRA!";
                _user = user;
            }
            RedirectToPage("./Home");
        }
        public IActionResult OnPost(Usuario user)
        {
            this._user = user;
            var IP = Request.Form["ip"];
            if (!ModelState.IsValid || string.IsNullOrEmpty(IP))
            {
                return RedirectToPage("./CheckEmail");
            }
            Input.ImageMimeType = Input.PhotoAvatar.ContentType;
            Input.ImageName = Path.GetFileName(Input.PhotoAvatar.FileName);
            using (var memoryStream = new MemoryStream())
            {
                Input.PhotoAvatar.CopyTo(memoryStream);
                Input.PhotoFile = memoryStream.ToArray();
            }

            _user.PhotoFile = Input.PhotoFile;
            _user.ImageName = Input.ImageName;
            _service.Update(this._user);
            return RedirectToPage("./Login");
        }
    }
}
