using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using ContactManager.Models;

namespace ContactManager.Models
{
    public class DomainViewModel
    {
        public List<Individual> Individuals;
        public SelectList Domains;
        public SelectList Services;
        public string SelectDomain { get; set; }
        public string SelectService { get; set; }
        public string SearchString { get; set; }
    }
}