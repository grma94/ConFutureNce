using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConFutureNce.Models
{
    public abstract class UserType
    {
        public int UserTypeID { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
