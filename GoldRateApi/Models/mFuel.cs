using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GoldRateApi.Models
{
    public class mFuel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(name: "iID", TypeName = "int", Order = 0)]
        public int iID { get; set; }
        [Column(name: "cFuel", TypeName = "Varchar(20)", Order = 1)]
        public string Fuel { get; set; }
        [Column(name: "rFuelPrice", TypeName = "decimal(9,3)", Order = 2)]
        public decimal FuelPrice { get; set; }
        [Column(name: "cFuelMonth", TypeName = "Varchar(20)", Order = 1)]
        public string FuelMonth { get; set; }
        [Column(name: "cFuelPriceChange", TypeName = "Varchar(20)", Order = 3)]
        public string FuelPriceChange { get; set; }

        [Column(name: "dUpdatedDateTime", TypeName = "datetime", Order = 4)]
        public DateTime? UpdatedDateTime { get; set; }
    }
}
