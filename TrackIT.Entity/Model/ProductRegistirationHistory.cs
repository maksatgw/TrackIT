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
        public int ProductRegistirationId { get; set; }
        public DateTime EndDate { get; set; }

        [ForeignKey("ProductRegistirationId")]
        public ProductRegistiration ProductRegistiration { get; set; }
    }
}
