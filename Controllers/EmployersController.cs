using FPTJob.Data;
using FPTJob.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FPTJob.Controllers
{
    [Authorize(Roles = "Employer")]
    public class EmployersController : Controller
    {
        public readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public EmployersController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index(ApplicationDbContext _context) 
        {
            var model = await _context.JobListings
                .Include(jl => jl.Employer)
                .Include(jl => jl.JobCategory)
                .Where(a => a.EmployerId == HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)
                .ToListAsync();
            return View(model);
        }
        public async Task<IActionResult> EmployerIndex()
        {
            var model = await _context.JobListings
                .Include(jl => jl.Employer)
                .Include(jl => jl.JobCategory)
                .Where(a => a.EmployerId == HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)
                .ToListAsync();
            return View(model);
        }
        public async Task<IActionResult> ViewJobApplications(int? jobId, string status)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {user.Id} cannot be found";
                return NotFound();
            }
            var jobListing = await _context.JobListings
                .Include(j => j.JobApplications)
                    .ThenInclude(js => js.JobSeeker)
                .FirstOrDefaultAsync(j => j.JobListingId == jobId && j.EmployerId == user.Id);

            if (jobListing == null)
            {
                return NotFound();
            }
            var filteredApplications = jobListing.JobApplications;
            if (!string.IsNullOrEmpty(status))
            {
                filteredApplications = filteredApplications.Where(a => a.Status == status).ToList();
            }
            var viewModel = new ViewJobApplicationViewModel
            {
                JobListing = jobListing,
                JobApplications = filteredApplications
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int jobApplicationId, string status)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {user.Id} cannot be found";
                return NotFound();
            }

            var jobApplication = await _context.JobApplications
                .Include(j => j.JobListing)
                .FirstOrDefaultAsync(j => j.JobApplicationId == jobApplicationId);
            if (jobApplication == null || jobApplication.JobListing == null || jobApplication.JobListing.EmployerId != user.Id)
            {
                return NotFound();
            }

            jobApplication.Status = status;
            await _context.SaveChangesAsync();

            return RedirectToAction("ViewJobApplications", new { jobId = jobApplication.JobListingId });
        }
        public async Task<IActionResult> ViewJobSeekerDetails(string jobSeekerId)
        {
            var user = await _userManager.FindByIdAsync(jobSeekerId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {user.Id} cannot be found";
                return NotFound();
            }

            return View(user);
        }
    }
}
