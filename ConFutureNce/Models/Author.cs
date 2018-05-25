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
        public string ScTitle { get; set; }
        [Required]
        public string OrgName { get; set; }

        public ICollection<Paper> Papers { get; set; }
    }
}
