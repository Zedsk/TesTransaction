using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TesTransaction.Data.Entity;

namespace TesTransaction.Models.Transactions
{
    public class TrIndexViewModel
    {
        [Required(ErrorMessage = "Un n° de terminal est nécessaire")]
        [DataType(DataType.Text)]
        public int TerminalId { get; set; }

        [Required(ErrorMessage = "Un n° de transaction est nécessaire")]
        [DataType(DataType.Text)]
        [Display(Name = "n° Opération")]
        public string NumTransaction { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Date")]
        public string DateDay { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Heure")]
        public string HourDay { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Vendeur")]
        public string Vendor { get; set; }

        [Required(ErrorMessage = "Un total tvac est nécessaire")]
        [DataType(DataType.Text)]
        [Display(Name = "TOTAL TVAC")]
        public string GlobalTotal { get; set; }

        //[Required(ErrorMessage = "TVA obligatoire")]
        [DataType(DataType.Text)]
        [Display(Name = "TVA")]
        public decimal GlobalVAT { get; set; }

        public IList<VAT> VatsList { get; set; }
        public IList<TrDetailsViewModel> DetailsListWithTot { get; set; }
    }
}