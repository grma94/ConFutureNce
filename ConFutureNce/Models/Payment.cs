using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConFutureNce.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public bool IsDone { get; set; }
        public int PaperId { get; set; }

        public Paper Paper { get; set; }
        public Invoice Invoice { get; set; }
    }
}
