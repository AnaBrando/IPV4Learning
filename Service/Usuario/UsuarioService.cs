using Domain;
using Domain.Interfaces.Application;
using Domain.Model;
using System;
using System.Net.Mail;


namespace Service.Usuario
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repo;
        public UsuarioService(IUsuarioRepository repo)
        {
            _repo = repo;
        }
        public void Add(Domain.Model.Usuario user)
        {
            if (user.EmailConfirmed == true)
            {          
                    this._repo.Add(user);         
            }          
        }
        public void Save()
        {
            throw new System.NotImplementedException();
        }
        public void Update(Domain.Model.Usuario user)
        {
            this._repo.Update(user);
        }

        public bool EmailIsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

      

       
    }
}
