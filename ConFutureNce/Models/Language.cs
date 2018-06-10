using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ConFutureNce.Models
{
    public class Language
    {
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }


        public ICollection<Paper> Papers { get; set; }
        public ICollection<Reviewer> ReviewersFirst {get; set; }
        public ICollection<Reviewer> ReviewersSecond { get; set; }
        public ICollection<Reviewer> ReviewersThird { get; set; }
        [NotMapped]
        public IEnumerable<Reviewer> AllReviewers
        {
            get { return ReviewersFirst.Concat(ReviewersSecond.Concat(ReviewersThird)); }
        }

    }
}
