using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FPTJob.Data;
using FPTJob.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Security.Claims;

namespace FPTJob.Controllers
{
    [Authorize(Roles = "Admin,Employer")]
    public class JobListingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public JobListingsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: JobListings
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var model = await _context.JobListings
                .Include(jl => jl.Employer)
                .Include(jl => jl.JobCategory)
                .ToListAsync();
            return View(model);
        }

        // GET: JobListings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobListing = await _context.JobListings
                .Include(j => j.Employer)
                .Include(j => j.JobCategory)
                .FirstOrDefaultAsync(m => m.JobListingId == id);
            if (jobListing == null)
            {
                return NotFound();
            }

            return View(jobListing);
        }

        // GET: JobListings/Create
        public IActionResult Create()
        {
            var approvedCategories = _context.JobCategories.Where(c => c.IsApproved);
            ViewData["EmployerId"] = new SelectList(_context.Set<Employer>(), "Id", "FirstName");
            ViewData["JobCategoryId"] = new SelectList(approvedCategories, "JobCategoryId", "JobCategoryName");
            return View();
        }

        // POST: JobListings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobListingId,JobTitle,JobDescription,JobRequirement,JobSalary,DeadLine,EmployerId,JobCategoryId")] JobListing jobListing)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login"); // Redirect to login if not authenticated
                }
                jobListing.EmployerId = user.Id;
                //jobListing.DeadLine = DateTime.Now;
                _context.Add(jobListing);
                await _context.SaveChangesAsync();
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index", "JobListings");
                }
                return RedirectToAction("EmployerIndex", "Employers");

            }
            ViewData["EmployerId"] = new SelectList(_context.Set<Employer>(), "Id", "FirstName", jobListing.EmployerId);
            ViewData["JobCategoryId"] = new SelectList(_context.JobCategories, "JobCategoryId", "JobCategoryName", jobListing.JobCategoryId);
            return View(jobListing);
        }

        // GET: JobListings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (id == null)
            {
                return NotFound();
            }

            var jobListing = await _context.JobListings.FindAsync(id);
            if (jobListing == null)
            {
                return NotFound();
            }
            var approvedCategories = _context.JobCategories.Where(c => c.IsApproved);
            ViewData["EmployerId"] = new SelectList(_context.Set<Employer>(), "Id", "FirstName", jobListing.EmployerId);
            ViewData["JobCategoryId"] = new SelectList(approvedCategories, "JobCategoryId", "JobCategoryName", jobListing.JobCategoryId);
            return View(jobListing);
        }

        // POST: JobListings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobListingId,JobTitle,JobDescription,JobRequirement,JobSalary,DeadLine,EmployerId,JobCategoryId")] JobListing jobListing)
        {
            if (id != jobListing.JobListingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                try
                {
                    jobListing.EmployerId = user.Id;
                    _context.Update(jobListing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobListingExists(jobListing.JobListingId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index", "JobListings");
                }
                return RedirectToAction("EmployerIndex", "Employers");
            }
            ViewData["EmployerId"] = new SelectList(_context.Set<Employer>(), "Id", "FirstName", jobListing.EmployerId);
            ViewData["JobCategoryId"] = new SelectList(_context.JobCategories, "JobCategoryId", "JobCategoryName", jobListing.JobCategoryId);
            return View(jobListing);
        }

        // GET: JobListings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobListing = await _context.JobListings
                .Include(j => j.Employer)
                .Include(j => j.JobCategory)
                .FirstOrDefaultAsync(m => m.JobListingId == id);
            if (jobListing == null)
            {
                return NotFound();
            }

            return View(jobListing);
        }

        // POST: JobListings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobListing = await _context.JobListings.FindAsync(id);
            if (jobListing != null)
            {
                _context.JobListings.Remove(jobListing);
            }

            await _context.SaveChangesAsync();
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "JobListings");
            }
            return RedirectToAction("EmployerIndex", "Employers");
        }

        private bool JobListingExists(int id)
        {
            return _context.JobListings.Any(e => e.JobListingId == id);
        }
    }
}
