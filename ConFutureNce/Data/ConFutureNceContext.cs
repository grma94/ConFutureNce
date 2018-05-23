using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ConFutureNce.Models;

namespace ConFutureNce.Models
{
    public class ConFutureNceContext : IdentityDbContext<ApplicationUser>
    {
        public ConFutureNceContext (DbContextOptions<ConFutureNceContext> options)
            : base(options)
        {
        }

        public DbSet<ConFutureNce.Models.Paper> Paper { get; set; }

        public DbSet<ConFutureNce.Models.Review> Review { get; set; }

        public DbSet<ConFutureNce.Models.Payment> Payment { get; set; }
    }
}
