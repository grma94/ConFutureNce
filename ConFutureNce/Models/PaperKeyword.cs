using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConFutureNce.Models
{
    public class PaperKeyword
    {
        [StringLength(40)]
        public string KeyWord { get; set; }
        public int PaperId { get; set; }

        public Paper Paper { get; set; }
    
    }
}
