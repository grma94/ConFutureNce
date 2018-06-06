using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConFutureNce.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        [Required]
        [MinLength(50)]
        public string Problems { get; set; }
        [Required]
        [MinLength(50)]
        public string WhyProblems { get; set; }
        [Required]
        [MinLength(50)]
        public string Solution { get; set; }
        [Required]
        [MinLength(50)]
        public string Achievements { get; set; }
        [Required]
        [MinLength(20)]
        public string NotMentioned { get; set; }
        [Required]
        [MinLength(50)]
        public string Grade { get; set; }
        public string GeneralComments { get; set; }
        public DateTime Date { get; set; }
        public int PaperId { get; set; }

        public Paper Paper { get; set; }
    }
}
