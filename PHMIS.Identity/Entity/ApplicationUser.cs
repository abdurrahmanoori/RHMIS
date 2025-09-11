using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PHMIS.Identity.Entity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }


        //public List<ApplicationRole>? ApplicationRoles { get; set; } 
    }
}
