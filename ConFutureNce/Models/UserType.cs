using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ConFutureNce.Models
{
    public abstract class UserType
    {
        public int UserTypeId { get; set; }
        [Required]
        public string ApplicationUserId {get;set;}

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
