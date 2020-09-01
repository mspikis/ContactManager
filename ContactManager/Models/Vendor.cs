using System;
using System.ComponentModel.DataAnnotations;

namespace ContactManager.Models
{
    public abstract class Vendor
    {
        public int ID { get; set; }
        [StringLength(300)]
        public string Address { get; set; }
        [Phone]
        public string Telephone { get; set; }
        [Display(Name = "E-mail")]
        [EmailAddress]
        public string EMail { get; set; }
        [StringLength(100)]
        public string Domain { get; set; }
        [StringLength(100)]
        public string Service { get; set; }
        [Range(1, 9999.99)]
        [DataType(DataType.Currency)]
        public float FeePerHour { get; set; }

    }
}
