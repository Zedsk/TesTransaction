using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TesTransaction.Data.Entity;

namespace TesTransaction.Models.Transactions
{
    public class TrPaymentMenuViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "n° Opération")]
        public string NumTransaction { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Montant à payer")]
        public string GlobalTotal { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Méthodes de paiement")]
        public string MethodP { get; set; }

        public IList<PAYMENT_METHOD> MethodsP { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Montant")]
        public string Amount { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Montant à rendre")]
        public string CashReturn { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Réponse du système")]
        public int Resp { get; set; }

        public TrTicketViewModel Ticket { get; set; }

        [DataType(DataType.Text)]
        public string NumTicket { get; set; }
    }
}