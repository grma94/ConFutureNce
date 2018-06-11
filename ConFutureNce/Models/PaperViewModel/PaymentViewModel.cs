using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConFutureNce.Models.PaperViewModel
{
    public class PaymentViewModel
    {
        public string UserName { get; set; }
        public string BillingAddress { get; set; }
        [Required]
        public string TaxNumber { get; set; }

        public int PaperId { get; set; }
    }
}
