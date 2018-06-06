using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConFutureNce.Models
{
    public class Author : UserType
    {
        [Required]
        [StringLength(10)]
        public string ScTitle { get; set; }
        [Required]
        [StringLength(100)]
        public string OrgName { get; set; }

        public ICollection<Paper> Papers { get; set; }
    }
}
