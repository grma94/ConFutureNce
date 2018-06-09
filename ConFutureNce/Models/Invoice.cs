using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConFutureNce.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string BillingAddress { get; set; }
        [Required]

        public string TaxNumber { get; set; }
        public int PaymentId { get; set; }

        public Payment Payment { get; set; }
    }
}
