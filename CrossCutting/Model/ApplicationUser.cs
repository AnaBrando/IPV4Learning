using Microsoft.AspNetCore.Identity;

namespace CrossCutting.Model
{
    public class ApplicationUser : IdentityUser
    {       
        public byte[] PhotoFile {  get; set; }
        public string ImageName { get; set; }
        public string ProfessorId { get; set; }
        public string RoleName { get; set; }
    }
}
