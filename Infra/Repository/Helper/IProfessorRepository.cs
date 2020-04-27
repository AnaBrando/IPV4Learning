using CrossCutting.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Repository.Helper
{
    public interface IProfessorRepository
    {
        List<ApplicationUser> GetProfessores();
        List<ApplicationUser> GetAlunos(string professorId);
    }
}
