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
        public string Problems { get; set; }
        [Required]
        public string WhyProblems { get; set; }
        [Required]
        public string Solution { get; set; }
        [Required]
        public string Achievements { get; set; }
        [Required]
        public string NotMentioned { get; set; }
        [Required]
        public string Grade { get; set; }
        public string GeneralComments { get; set; }
        public DateTime Date { get; set; }
        public int PaperId { get; set; }

        public Paper Paper { get; set; }
    }
}
