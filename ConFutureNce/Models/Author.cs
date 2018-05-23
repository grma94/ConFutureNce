using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConFutureNce.Models
{
    public class Author : UserType
    {
        public string ScTitle { get; set; }
        public string OrgName { get; set; }

        public virtual ICollection<Paper> Papers { get; set; }
    }
}
