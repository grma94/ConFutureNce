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
        [Required]
        [StringLength(200)]
        public string TitleENG { get; set; }
        [Required]
        [StringLength(200)]
        public string TitleORG { get; set; }
        [Required]
        [StringLength(200)]
        public string Authors { get; set; }
        [Required]
        [StringLength(1000)]
        public string Abstract { get; set; }
        [Required]
        [StringLength(100)]
        public string OrgName { get; set; }
        public DateTime SubmissionDate { get; set; }
        public ProcessStatus Status { get; set; }
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

        public enum ProcessStatus
        {
            Submitted,
            UnderReview,
            Reviewed,
            Qualified,
            Unqualified
        };

        public string KeywordsToString
        {
            get
            {
                string container = string.Empty;

                foreach (var paperKeyword in PaperKeywords)
                {
                    container += paperKeyword.KeyWord + ", ";
                }

                return container.TrimEnd( new[] {' ', ','});
            }
        }
    }
}
