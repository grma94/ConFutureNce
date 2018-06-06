using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ConFutureNce.Models
{
    public class Reviewer : UserType
    {
        public string ScTitle { get; set; }
        public string OrgName { get; set; }
        [Required]
        public int Language1Id { get; set; }

        public int? Language2Id { get; set; }

        public int? Language3Id { get; set; }

        [ForeignKey("Language1Id")]
        public Language Language1 { get; set; }
        [ForeignKey("Language2Id")]
        public Language Language2 { get; set; }
        [ForeignKey("Language3Id")]
        public Language Language3 { get; set; }

        public ICollection<Paper> Papers { get; set; }
    }
}
