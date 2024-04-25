using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FPTJob.Data;
using FPTJob.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Builder;

namespace FPTJob.Controllers
{
    [Authorize(Roles = "JobSeeker")]
    public class JobApplicationsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public JobApplicationsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> EditResume(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {user.Id} cannot be found";
                return NotFound();
            }
            var jobApplication = _context.JobApplications.FirstOrDefault(j => j.JobApplicationId == id && j.JobSeekerId == user.Id);
            if (jobApplication == null)
            {
                return NotFound();
            }
            ViewData["JobSeekerId"] = _userManager.GetUserId(User);
            ViewData["JobId"] = id;
            return View(jobApplication);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditResume(int id, string resume)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {user.Id} cannot be found";
                return NotFound();
            }
            var jobApplication = _context.JobApplications.FirstOrDefault(j => j.JobApplicationId == id && j.JobSeekerId == user.Id);
            if (jobApplication == null)
            {
                return NotFound();
            }
            jobApplication.Resume = resume;
            _context.Update(jobApplication);
            await _context.SaveChangesAsync();
            return Redirect("/JobApplications/JobApplicationIndex");
        }
        // GET: JobApplications
        [Authorize(Roles = "JobSeeker")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.JobApplications.Include(j => j.JobSeeker);
            return View(await applicationDbContext.ToListAsync());
        }
        
        public async Task<IActionResult> JobApplicationIndex()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {user.Id} cannot be found";
                return NotFound();
            }
            var jobApplications = _context.JobApplications
                .Include(jl => jl.JobListing)
                .Where(j => j.JobSeekerId == user.Id)
                .ToList();
            return View(jobApplications);
        }
        // GET: JobApplications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {user.Id} cannot be found";
                return NotFound();
            }
            if (id == null)
            {
                return NotFound();
            }

            var jobApplication = await _context.JobApplications
                .Include(j => j.JobSeeker)
                .Include(jl => jl.JobListing)
                .FirstOrDefaultAsync(m => m.JobApplicationId == id);
            if (jobApplication == null)
            {
                return NotFound();
            }

            return View(jobApplication);
        }

        // GET: JobApplications/Create
        public IActionResult Create(int? id)
        {
            ViewData["JobSeekerId"] = _userManager.GetUserId(User);
            ViewData["JobId"] = id;
            return View();
        }

        // POST: JobApplications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id,[Bind("JobApplicationId,Resume,JobListingId,JobSeekerId")] JobApplication jobApplication)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {user.Id} cannot be found";
                return NotFound();
            }
            if (id != jobApplication.JobApplicationId)
            {
                return NotFound();
            }
            ModelState.Remove("Status");
            string status = "Applied";
            if (ModelState.IsValid)
            {
                var newjA = new JobApplication()
                {
                    Resume = jobApplication.Resume,
                    Status = status,
                    JobListingId = jobApplication.JobListingId,
                    JobSeekerId = jobApplication.JobSeekerId
                };
                _context.JobApplications.Add(jobApplication);
                await _context.SaveChangesAsync();
                return Redirect("/JobApplications/JobApplicationIndex");
            }
            return View(jobApplication);
        }

        // GET: JobApplications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobApplication = await _context.JobApplications.FindAsync(id);
            if (jobApplication == null)
            {
                return NotFound();
            }
            ViewData["JobSeekerId"] = _userManager.GetUserId(User);
            ViewData["JobId"] = id;
            return View(jobApplication);
        }

        // POST: JobApplications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobApplicationId,Resume,Status,JobListingId,JobSeekerId")] JobApplication jobApplication)
        {
            if (id != jobApplication.JobApplicationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobApplication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobApplicationExists(jobApplication.JobApplicationId))
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
            ViewData["JobSeekerId"] = new SelectList(_context.Set<JobSeeker>(), "Id", "Id", jobApplication.JobSeekerId);
            return View(jobApplication);
        }

        // GET: JobApplications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobApplication = await _context.JobApplications
                .Include(j => j.JobSeeker)
                .FirstOrDefaultAsync(m => m.JobApplicationId == id);
            if (jobApplication == null)
            {
                return NotFound();
            }

            return View(jobApplication);
        }

        // POST: JobApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobApplication = await _context.JobApplications.FindAsync(id);
            if (jobApplication != null)
            {
                _context.JobApplications.Remove(jobApplication);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("JobApplicationIndex");
        }

        private bool JobApplicationExists(int id)
        {
            return _context.JobApplications.Any(e => e.JobApplicationId == id);
        }

    }
}
