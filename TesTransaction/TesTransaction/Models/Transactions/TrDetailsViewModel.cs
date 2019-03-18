using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TesTransaction.Models.Transactions
{
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

        [Required(ErrorMessage = "Il faut saisir un produit")]
        [DataType(DataType.Text)]
        public string AddProduct { get; set; }

        public bool Minus { get; set; }

        public IList<TrDetailsViewModel> DetailsListWithTot { get; set; }
    }
}