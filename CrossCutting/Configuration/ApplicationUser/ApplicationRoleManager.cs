using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CrossCutting.Configuration
{
    public class ApplicationRoleManager : IdentityRole<string>
    {
        public ICollection<ApplicationRoleManager> UserRoles { get; set; }
        
        public virtual ApplicationRoleManager Role{get; set;}
    }
}
