using CrossCutting.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.ModelView
{
    public class ProfessorAlunosModelView
    {
        public List<ApplicationUser> alunos;
        public ProfessorAlunosModelView()
        {
            alunos = new List<ApplicationUser>();
        }
      
    }
}
