using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConFutureNce.Models
{
    public class Paper
    {
        public int PaperID { get; set; }
        public string TitleENG { get; set; }
        public string TitleORG { get; set; }
        public string Authors { get; set; }
        public string Abstract { get; set; }
        public string OrgName { get; set; }
        public DateTime SubmissionDate { get; set; }
        public enum ProcessStatus { Submitted, UnderReview, Reviewed, Qualified, Unqualified};

        public virtual ICollection<PaperKeyword> PaperKeywords { get; set; }
        public virtual Language Language { get; set; }
        public virtual Review Review { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual Author Author { get; set; }
        public virtual Reviewer Reviewer { get; set; }
    }
}
