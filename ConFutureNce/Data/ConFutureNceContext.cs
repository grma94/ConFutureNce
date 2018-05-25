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


        public DbSet<ConFutureNce.Models.Review> Review { get; set; }

        public DbSet<ConFutureNce.Models.Payment> Payment { get; set; }

        public DbSet<PaperKeyword> PaperKeyword { get; set; }

        public DbSet<Reviewer> Reviewer { get; set; }

        public DbSet<ProgrammeCommitteeMember> ProgrammeCommitteeMember { get; set; }

        public DbSet<Organizer> Organizer { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<PaperKeyword>().HasKey(p => new { p.KeyWord, p.PaperId });
            /*    builder.Entity<Paper>()
                    .HasOne(p => p.Language).WithMany(l => l.Papers).OnDelete(DeleteBehavior.Restrict);
                builder.Entity<Language>()
        .HasMany(l => l.Papers).WithOne(p => p.Language).OnDelete(DeleteBehavior.Restrict);*/
            builder.Entity<Language>()
                .HasMany(r => r.ReviewersFirst).WithOne(e => e.Language1).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Language>()
                 .HasMany(r => r.ReviewersSecond).WithOne(e => e.Language2).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Language>()
                .HasMany(r => r.ReviewersThird).WithOne(e => e.Language3).OnDelete(DeleteBehavior.Restrict);
                
        }

        public DbSet<ConFutureNce.Models.Paper> Paper { get; set; }
    }
}
