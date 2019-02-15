using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesTransaction.Data.Entity;

namespace TesTransaction.Models
{
    public class TrIndexViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Nom de caisse")]
        public string TerminalName { get; set; }

        public IList<string> TerminalsNames { get; set; }
        public IList<TERMINAL> TerminalsList { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public int? TerminalId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "n° Opération")]
        public string NumTransaction { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date")]
        public string DateDay { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Heure")]
        public string HourDay { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Vendeur")]
        public string Vendor { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "TOTAL TVAC")]
        public decimal GlobalTotal { get; set; }

        public IList<VAT> VatsList { get; set; }
    }

    public class TrDetailsViewModel
    {
        
        [DataType(DataType.Text)]
        [Display(Name = "Produit")]
        public string ProductName { get; set; }

       
        [DataType(DataType.Text)]
        [Display(Name = "Prix")]
        public decimal Price { get; set; }

        
        [DataType(DataType.Text)]
        [Display(Name = "Qtité")]
        public int Quantity { get; set; }

        
        [DataType(DataType.Text)]
        [Display(Name = "Remise")]
        public decimal? Discount { get; set; }

        
        [DataType(DataType.Text)]
        [Display(Name = "TVA")]
        public decimal? ProductVat { get; set; }

        
        [DataType(DataType.Text)]
        [Display(Name = "Total")]
        public decimal? Total { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Remise globale (%)")]
        [Range(0, 100, ErrorMessage = "valeur devant être comprise entre 0 et 100")]
        public decimal? GlobalDiscount { get; set; }

        public IList<TRANSACTION_DETAILS> DetailsListById { get; set; }
        public IList<TrDetailsViewModel> DetailsListWithTot { get; set; }
        //public IList<TRANSACTION_DETAILS> TransactionDetailsListById { get; set; }
    }
}