using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Entities
{
    public class Usuario : IdentityUser
    {
        public bool EstaActivo { get; set; }
    }
}
