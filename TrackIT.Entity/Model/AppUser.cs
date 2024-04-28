using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackIT.Entity.Model;

namespace TrackIT.Business.Model
{
    public class AppUser : IdentityUser
    {
        public bool isActive { get; set; } = true;
        public List<ProductRegistiration> ProductRegistrations { get; set; }
    }
}
