using System;
using System.ComponentModel.DataAnnotations;
namespace ContactManager.Models

{
    public class Individual: Vendor
    {
        [Required]
        [RegularExpression(@"^([A-Za-z]{2,25})*$", ErrorMessage = "Please enter a valid Forename.")]
        public string Forename { get; set; }
        [Required]
        [StringLength(30)]
        [RegularExpression(@"^([A-Za-z]{2,25})*$", ErrorMessage = "Please enter a valid Surname.")]
        public string Surname { get; set; }
    }
}
