
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cerveceria2._0.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsEnabled { get; set; }


    }
}