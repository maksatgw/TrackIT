using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackIT.Business.Model;

namespace TrackIT.Entity.Model
{
    public class ProductRegistirationHistory
    {
        public int ProductRegistirationHistoryId { get; set; }
        public int ProductId { get; set; }
        public string AppUserId { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [ForeignKey("AppUserId")]
        public AppUser AppUser { get; set; }
    }
}
