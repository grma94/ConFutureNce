﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConFutureNce.Models
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public bool IsDone { get; set; }

        public virtual Paper Paper { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}
