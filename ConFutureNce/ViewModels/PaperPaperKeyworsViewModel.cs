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
        [Display(Name = "English title")]
        public string TitleENG { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Title")]
        public string TitleORG { get; set; }
        [Required]
        [StringLength(200)]
        public string Authors { get; set; }
        [Required]
        [StringLength(1000)]
        public string Abstract { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Organization's name")]
        public string OrgName { get; set; }
        public DateTime SubmissionDate { get; set; }
        [Display(Name = "File")]
        public byte[] PaperFile { get; set; }
        public int LanguageId { get; set; }
        public int AuthorId { get; set; }

        [Display(Name = "Keywords")]
        public string PaperKeywords { get; set; }
        public Models.Language Language { get; set; }

    }
}
