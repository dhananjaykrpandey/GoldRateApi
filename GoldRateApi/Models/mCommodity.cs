using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GoldRateApi.Models
{
    public class mCommodity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(name: "iID", TypeName = "int", Order = 0)]
        public int iID { get; set; }
        [Column(name: "cCommodity", TypeName = "Varchar(20)", Order = 1)]
        public string Commodity { get; set; }
        [Column(name: "rCommodityPrice", TypeName = "decimal(9,3)", Order = 2)]
        public decimal CommodityPrice { get; set; }
        [Column(name: "cCommodityPriceChange", TypeName = "Varchar(20)", Order = 3)]
        public string CommodityPriceChange { get; set; }

        [Column(name: "dUpdatedDateTime", TypeName = "datetime", Order = 4)]
        public DateTime? UpdatedDateTime { get; set; }
    }
}
