using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackIT.Entity.Model;

namespace TrackIT.DTO.Dtos.UserDtos
{
    public class UserGetDto 
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public List<ProductRegistiration> ProductRegistirations { get; set; }
        public int? UserProductCount { get; set; }
        public List<string> Roles { get; set; }
    }
}
