using System;
namespace ContactManager.Models
{
    public class Company: Vendor
    {
        public string Name { get; set; }
        public string Website { get; set; }
    }
}
