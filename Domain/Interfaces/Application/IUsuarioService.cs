using Domain.Model;
namespace Domain.Interfaces.Application
{
    public interface IUsuarioService
    {
        void Add(Usuario user);
        void Update(Usuario user);
        void Save();
        bool EmailIsValid(string x);
  
    
    }
}