using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConFutureNce.Models.PaperViewModel
{
    public class AssignReviewerViewModel
    {
        public int LangId { get; set; }
        public IEnumerable<ReviewerVM> reviewersPerLang { get; set; }
    }

    public class ReviewerVM
    {
        public string ReviewerName { get; set; }
        public int ReviewerId { get; set; }
    }
}
