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
        [MinLength(20)]
        public string Problems { get; set; }
        [Required]
        [MinLength(20)]
        [Display(Name = "Why is a problem?")]
        public string WhyProblems { get; set; }
        [Required]
        [MinLength(20)]
        public string Solution { get; set; }
        [Required]
        [MinLength(20)]
        public string Achievements { get; set; }
        [Required]
        [MinLength(20)]
        [Display(Name = "What wasn't mentioned?")]
        public string NotMentioned { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(3)]
        [Display(Name = "Grade [0-100]")]
        public string Grade { get; set; }
        [Display(Name = "General comments")]
        public string GeneralComments { get; set; }
        public DateTime Date { get; set; }
        public int PaperId { get; set; }

        public Paper Paper { get; set; }
    }
}
