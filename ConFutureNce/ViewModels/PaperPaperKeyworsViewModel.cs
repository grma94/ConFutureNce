using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConFutureNce.ViewModels
{
    public class PaperPaperKeyworsViewModel
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
        public byte[] PaperFile { get; set; }
        public int LanguageId { get; set; }
        public int AuthorId { get; set; }

        public string PaperKeywords { get; set; }
        public Language Language { get; set; }

    }
}
