using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackIT.Business.Model;

namespace TrackIT.Entity.Model
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Serial { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public bool isActive { get; set; } = true;
        public DateTime DateAdded { get; set; } = DateTime.Now;

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

    }
}
