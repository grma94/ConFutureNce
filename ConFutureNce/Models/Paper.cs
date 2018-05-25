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
        public int PaperId { get; set; }
        public string TitleENG { get; set; }
        public string TitleORG { get; set; }
        public string Authors { get; set; }
        public string Abstract { get; set; }
        public string OrgName { get; set; }
        public DateTime SubmissionDate { get; set; }
        public enum ProcessStatus { Submitted, UnderReview, Reviewed, Qualified, Unqualified};
        public byte[] PaperFile { get; set; }
        public int LanguageId { get; set; }
        public int AuthorId { get; set; }
        public int? ReviewerId { get; set; }

        public ICollection<PaperKeyword> PaperKeywords { get; set; }
        [ForeignKey("LanguageId")]
        public Language Language { get; set; }
        public Review Review { get; set; }
        public Payment Payment { get; set; }
        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
        [ForeignKey("ReviewerId")]
        public Reviewer Reviewer { get; set; }
    }
}
