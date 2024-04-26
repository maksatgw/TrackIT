using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackIT.Entity.Model
{
    public class ProductAsset
    {
        public int ProductAssetId { get; set; }
        public int ProductId { get; set; }
        public string AssetUrl { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
