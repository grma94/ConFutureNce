using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConFutureNce.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public string Name { get; set; }
        public string BillingAddress { get; set; }
        public string TaxNumber { get; set; }

        public virtual Payment Payment { get; set; }
    }
}
