using FPTJob.Data;
using FPTJob.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FPTJob.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context; 
        }
        [HttpGet]
        public async Task<IActionResult> IndexAsync(string jobTitle, string categoryName)
        {
            var jobListingsQuery = _context.JobListings.AsQueryable();
            if (!string.IsNullOrEmpty(jobTitle))
            {
                jobListingsQuery = jobListingsQuery.Where(j => j.JobTitle.Contains(jobTitle));
            }
            if (!string.IsNullOrEmpty(categoryName))
            {
                jobListingsQuery = jobListingsQuery.Include(j => j.JobCategory)
                                                   .Where(j => j.JobCategory.JobCategoryName.Contains(categoryName));
            }
            var jobListings = await jobListingsQuery.Include(j => j.Employer)
                                                    .Include(j => j.JobCategory)
                                                    .ToListAsync();

            return View(jobListings);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult SearchJobs(string jobTitle, string categoryName)
        {
            var jobListings = _context.JobListings
                .Where(j => j.JobTitle.Contains(jobTitle) &&
                            j.JobCategory.JobCategoryName.Contains(categoryName))
                .ToList();

            return View(jobListings);
        }
    }
}
