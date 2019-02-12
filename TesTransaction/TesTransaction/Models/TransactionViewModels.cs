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

        //[Required]
        //[DataType(DataType.Text)]
        //[Display(Name = "Produit")]
        //public string ProductName { get; set; }

        //[Required]
        //[DataType(DataType.Text)]
        //[Display(Name = "Prix")]
        //public string Price { get; set; }

        //[Required]
        //[DataType(DataType.Text)]
        //[Display(Name = "Qtité")]
        //public string Quantity { get; set; }

        //[Required]
        //[DataType(DataType.Text)]
        //[Display(Name = "Remise")]
        //public string Discount { get; set; }

        //[Required]
        //[DataType(DataType.Text)]
        //[Display(Name = "TVA")]
        //public string ProductVat { get; set; }

        //[Required]
        //[DataType(DataType.Text)]
        //[Display(Name = "Total")]
        //public string Total { get; set; }

        //public IList<TRANSACTION_DETAILS> TransactionDetailsListById { get; set; }

    }

    public class TrDetailsViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Produit")]
        public string ProductName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Prix")]
        public decimal Price { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Qtité")]
        public int Quantity { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Remise")]
        public decimal? Discount { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "TVA")]
        public decimal? ProductVat { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Total")]
        public decimal? Total { get; set; }

        public IList<TRANSACTION_DETAILS> DetailsListById { get; set; }
        public IList<TrDetailsViewModel> DetailsListWithTot { get; set; }
        //public IList<TRANSACTION_DETAILS> TransactionDetailsListById { get; set; }
    }
}