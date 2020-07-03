
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GoldRateApi.Models
{
    public class mGoldRate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(name: "iID", TypeName = "int", Order = 0)]
        public int iID { get; set; }
        [Column(name:"cGoldCarat", TypeName ="Varchar(20)",Order =1)]
        public string GoldCarat { get; set; }
        [Column(name: "rGoldPriceMorning", TypeName = "decimal(9,3)", Order = 2)]
        public decimal GoldPriceMorning { get; set; }
        [Column(name: "rGoldPriceAfternoon", TypeName = "decimal(9,3)", Order = 2)]
        public decimal GoldPriceAfternoon { get; set; }
        [Column(name: "rGoldPriceEvening", TypeName = "decimal(9,3)", Order = 2)]
        public decimal GoldPriceEvening { get; set; }
        [Column(name: "rGoldPriceYesterday", TypeName = "decimal(9,3)", Order = 2)]
        public decimal GoldPriceYesterday { get; set; }
        [Column(name: "dUpdatedDateTime", TypeName = "datetime", Order = 3)]
        public DateTime? UpdatedDateTime { get; set; }
    }
}
