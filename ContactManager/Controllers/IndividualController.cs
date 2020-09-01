using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContactManager.Models;



namespace ContactManager.Controllers
{
    public class IndividualController : Controller
    {
        private readonly ContactManagerContext _context;

        public IndividualController(ContactManagerContext context)
        {
            _context = context;
        }

        // GET: Individual
        public async Task<IActionResult> Index(string selectDomain, string selectService, string searchString, string sortOrder)
        {
            //SortedSet list by order
            ViewBag.SurnameSortParm = String.IsNullOrEmpty(sortOrder) ? "surname_desc" : "";
            ViewBag.ForenameSortParm = sortOrder == "Forename" ? "forename_desc" : "Forename";
            ViewBag.AddressSortParm = sortOrder == "Address" ? "address_desc" : "Address";
            ViewBag.DomainSortParm = sortOrder == "Domain" ? "domain_desc" : "Domain";
            ViewBag.ServiceSortParm = sortOrder == "Service" ? "service_desc" : "Service";
            ViewBag.FeeSortParm = sortOrder == "Fee" ? "fee_desc" : "Fee";

            // Use LINQ to get list of Domains.
            IQueryable<string> domainQuery = from d in _context.Individual
                                             orderby d.Domain
                                             select d.Domain;
            // Use LINQ to get list of Services.
            IQueryable<string> serviceQuery = from s in _context.Individual
                                              orderby s.Service
                                              select s.Service;

            var individuals = from i in _context.Individual
                              select i;

            switch (sortOrder)
            {
                case "surname_desc":
                    individuals = individuals.OrderByDescending(s => s.Surname);
                    break;
                case "Forename":
                    individuals = individuals.OrderBy(s => s.Forename);
                    break;
                case "forename_desc":
                    individuals = individuals.OrderByDescending(s => s.Forename);
                    break;
                case "Address":
                    individuals = individuals.OrderBy(s => s.Address);
                    break;
                case "address_desc":
                    individuals = individuals.OrderByDescending(s => s.Address);
                    break;
                case "Domain":
                    individuals = individuals.OrderBy(s => s.Domain);
                    break;
                case "domain_desc":
                    individuals = individuals.OrderByDescending(s => s.Domain);
                    break;
                case "Service":
                    individuals = individuals.OrderBy(s => s.Service);
                    break;
                case "service_desc":
                    individuals = individuals.OrderByDescending(s => s.Service);
                    break;
                case "Fee":
                    individuals = individuals.OrderBy(s => s.FeePerHour);
                    break;
                case "fee_desc":
                    individuals = individuals.OrderByDescending(s => s.FeePerHour);
                    break;
                default:
                    individuals = individuals.OrderBy(s => s.Surname);
                    break;
            }
            // Search for Name or Forename
            if (!String.IsNullOrEmpty(searchString))
            {

                individuals = individuals.Where(s => s.Surname.Contains(searchString) || s.Forename.Contains(searchString));

            }

            //Search for Domain
            if (!String.IsNullOrEmpty(selectDomain))
            {
                individuals = individuals.Where(x => x.Domain == selectDomain);

            }
            if (!String.IsNullOrEmpty(selectService))
            {
                if (!String.IsNullOrEmpty(selectDomain))
                {
                    individuals = individuals.Where(x => x.Domain == selectDomain && x.Service == selectService);

                }
                else
                {
                    individuals = individuals.Where(x => x.Service == selectService);
                }

            }

            var individualDomainVM = new DomainViewModel
            {
                Domains = new SelectList(await domainQuery.Distinct().ToListAsync()),
                Services = new SelectList(await serviceQuery.Distinct().ToListAsync()),
                Individuals = await individuals.ToListAsync(),
                SearchString = searchString,
                SelectDomain = selectDomain,
                SelectService = selectService
                };

            return View(individualDomainVM);
        }

        // GET: Individual/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var individual = await _context.Individual
                .FirstOrDefaultAsync(m => m.ID == id);
            if (individual == null)
            {
                return NotFound();
            }

            return View(individual);
        }

        // GET: Individual/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Individual/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Forename,Surname,ID,Address,Telephone,EMail,Domain,Service,FeePerHour")] Individual individual)
        {
            if (ModelState.IsValid)
            {
                _context.Add(individual);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(individual);
        }

        // GET: Individual/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var individual = await _context.Individual.FindAsync(id);
            if (individual == null)
            {
                return NotFound();
            }
            return View(individual);
        }

        // POST: Individual/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Forename,Surname,ID,Address,Telephone,EMail,Domain,Service,FeePerHour")] Individual individual)
        {
            if (id != individual.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(individual);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IndividualExists(individual.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(individual);
        }

        // GET: Individual/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var individual = await _context.Individual
                .FirstOrDefaultAsync(m => m.ID == id);
            if (individual == null)
            {
                return NotFound();
            }

            return View(individual);
        }

        // POST: Individual/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var individual = await _context.Individual.FindAsync(id);
            _context.Individual.Remove(individual);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IndividualExists(int id)
        {
            return _context.Individual.Any(e => e.ID == id);
        }
    }
}
