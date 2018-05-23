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
        public int ReviewID { get; set; }
        public string Problems { get; set; }
        public string WhyProblems { get; set; }
        public string Solution { get; set; }
        public string Achievements { get; set; }
        public string NotMentioned { get; set; }
        public string Grade { get; set; }
        public string GeneralComments { get; set; }
        public DateTime Date { get; set; }

        public virtual Paper Paper { get; set; }
    }
}
