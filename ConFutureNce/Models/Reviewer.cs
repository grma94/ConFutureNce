using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConFutureNce.Models
{
    public class Reviewer : UserType
    {
        public string ScTitle { get; set; }
        public string OrgName { get; set; }

        public virtual Language Language1 { get; set; }
        public virtual Language Language2 { get; set; }
        public virtual Language Language3 { get; set; }
        public virtual ICollection<Paper> Papers { get; set; }
    }
}
