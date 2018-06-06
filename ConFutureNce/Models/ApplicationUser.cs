using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ConFutureNce.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Surname { get; set; }
        public string Address { get; set; }
        [Required]
        public string ConferenceName { get; set; }

        [ForeignKey("ConferenceName")]
        public Conference Conference { get; set; }
        public ICollection<UserType> Users {get;set;}
    }
}
