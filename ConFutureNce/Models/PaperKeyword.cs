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
        [Key, Column(Order = 0)]
        public string KeyWord { get; set; }
        [Key, Column(Order = 1)]
        public virtual Paper Paper { get; set; }
    
    }
}
