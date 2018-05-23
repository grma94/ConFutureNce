using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConFutureNce.Models
{
    public abstract class UserType : ApplicationUser
    {
        public int UserTypeID { get; set; }
    }
}
