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
        [Required(ErrorMessage = "Un n° de terminal est nécessaire")]
        [DataType(DataType.Text)]
        public int? TerminalId { get; set; }

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

        [DataType(DataType.Text)]
        [Display(Name = "TVA")]
        public decimal GlobalVAT { get; set; }

        public IList<VAT> VatsList { get; set; }
        public IList<TrDetailsViewModel> DetailsListWithTot { get; set; }
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
        [Display(Name = "remise")]
        public decimal? Discount { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "tva")]
        public decimal? ProductVat { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "total")]
        public decimal? TotalItem { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Remise globale (%)")]
        [Range(0, 100, ErrorMessage = "valeur devant être comprise entre 0 et 100")]
        public decimal? GlobalDiscount { get; set; }

        [Required(ErrorMessage = "Il faut saisir un produit")]
        [DataType(DataType.Text)]
        public string AddProduct { get; set; }

        public bool Minus { get; set; }

        public IList<TrDetailsViewModel> DetailsListWithTot { get; set; }
    }

    public class TrPaymentViewModel
    {
        public int TerminalId { get; set; }

        public string NumTransaction { get; set; }

        public string Vendor { get; set; }

        public decimal GlobalVAT { get; set; }

        public string GlobalTotal { get; set; }

        public decimal? GlobalDiscount { get; set; }

        public IList<PAYMENT_METHOD> PMethods { get; set; }
    }
}