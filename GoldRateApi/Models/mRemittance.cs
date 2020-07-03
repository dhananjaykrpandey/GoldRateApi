using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GoldRateApi.Models
{
    public class mRemittance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(name: "iID", TypeName = "int", Order = 0)]
        public int iID { get; set; }
        [Column(name: "cCurrencies", TypeName = "Varchar(20)", Order = 1)]
        public string Currencies { get; set; }
        [Column(name: "rCurrenciesMorning", TypeName = "decimal(9,3)", Order = 2)]
        public decimal CurrenciesMorning { get; set; }
        [Column(name: "rCurrenciesAfternoon", TypeName = "decimal(9,3)", Order = 2)]
        public decimal CurrenciesAfternoon { get; set; }
        [Column(name: "rCurrenciesEvening", TypeName = "decimal(9,3)", Order = 2)]
        public decimal CurrenciesEvening { get; set; }
        [Column(name: "rCurrenciesYesterday", TypeName = "decimal(9,3)", Order = 2)]
        public decimal CurrenciesYesterday { get; set; }
        [Column(name: "dUpdatedDateTime", TypeName = "datetime", Order = 3)]
        public DateTime? UpdatedDateTime { get; set; }
    }
}
