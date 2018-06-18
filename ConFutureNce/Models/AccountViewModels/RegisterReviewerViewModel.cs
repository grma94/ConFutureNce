using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConFutureNce.Models.AccountViewModels
{
    public class RegisterReviewerViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Oranization's name")]
        public string OrgName { get; set; }
        [Required]
        [StringLength(10)]
        [Display(Name = "Scientific title")]
        public string ScTitle { get; set; }

        [Display(Name = "Language 1")]
        public int Language1Id { get; set; }
        [Display(Name = "Language 2")]
        public int? Language2Id { get; set; }
        [Display(Name = "Language 1")]
        public int? Language3Id { get; set; }
    }
}