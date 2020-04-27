using Domain;
using Domain.Model;
using Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private UserDbContext _context;
        public UsuarioRepository(UserDbContext userContext)
        {
            this._context = userContext;
        }

        public void Add(Usuario e)
        {

            throw new NotImplementedException();

        }

        public Task Delete(Usuario e)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Usuario>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Usuario GetById(string id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Usuario e)
        {
            try
            {
                var updateUser = _context.Usuarios.Where(x => x.Email == e.Email).FirstOrDefault();
                updateUser.EmailConfirmed = true;
                updateUser.ImageName = e.ImageName;
                updateUser.PhotoFile = e.PhotoFile;
                _context.Usuarios.Update(updateUser);
                _context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error:" + ex);
                return false;
            }
                
        }
    }
}
