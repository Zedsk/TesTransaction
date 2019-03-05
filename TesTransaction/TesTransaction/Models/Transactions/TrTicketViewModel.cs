using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TesTransaction.Data.Entity;

namespace TesTransaction.Models.Transactions
{
    public class TrTicketViewModel
    {
        [DataType(DataType.DateTime)]
        [Display(Name = "Date")]
        public string DateTicket { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Langue")]
        public string Language { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "N° du ticket")]
        public string Ticket { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "N° de la transaction")]
        public string Transaction { get; set; }

        public IList<TrDetailsViewModel> DetailsListWithTot { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Remise")]
        public string DiscountG { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "TVA")]
        public string VatG { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Total")]
        public string TotalG { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Montant")]
        public string AmountP { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Méthode de paiment")]
        public string MethodP { get; set; }

        public IList<PAYMENT> Payments { get; set; }

        [DataType(DataType.Text)]
        public string Message { get; set; }

        [DataType(DataType.Text)]
        public string MessageId { get; set; }

        ////------
        //public SHOP Shop { get; set; }

        //public SHOP_TRANSLATION Shop_Translation { get; set; }
    }
}