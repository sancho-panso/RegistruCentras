using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace RegistruCentras.Domains
{
    public class AppUser: IdentityUser
    {
        public string Name { get; set; }

    }
}
