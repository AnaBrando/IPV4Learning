using CrossCutting.Model;
using Infra.Context;
using Infra.Repository.Helper;
using System.Collections.Generic;
using System.Linq;
namespace Infra.Repository
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly UserDbContext _context;
        public ProfessorRepository(UserDbContext userContext)
        {
            this._context = userContext;
        }

        public List<ApplicationUser> GetProfessores()
        {
            var professores = this._context.Users.Where(x => x.RoleName.Equals("Professor")).ToList();
            return professores;
        }
        public List<ApplicationUser> GetAlunos(string professorId)
        {
            if (!string.IsNullOrEmpty(professorId))
            {
                var alunos = this._context.Users.Where(x => x.ProfessorId == professorId).ToList();
                return alunos;
            }
            return null;
        }
    }
}
