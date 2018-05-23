using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConFutureNce.Models
{
    public class Conference
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime PaperDeadline { get; set; }
        public DateTime ReviewDeadline { get; set; }
        public DateTime SelectionDeadline { get; set; }
        public DateTime AssignDeadline { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
