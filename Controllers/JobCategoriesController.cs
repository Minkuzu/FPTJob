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

namespace FPTJob.Controllers
{
    [Authorize(Roles = "Admin, Employer")]
    public class JobCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JobCategories
        [Authorize(Roles = "Admin, Employer")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.JobCategories.ToListAsync());
        }

        // GET: JobCategories/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobCategory = await _context.JobCategories
                .FirstOrDefaultAsync(m => m.JobCategoryId == id);
            if (jobCategory == null)
            {
                return NotFound();
            }

            return View(jobCategory);
        }

        // GET: JobCategories/Create
        [Authorize(Roles = "Admin, Employer")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobCategoryId,JobCategoryName")] JobCategory jobCategory)
        {
            if (ModelState.IsValid)
            {
                jobCategory.IsApproved = false;
                _context.Add(jobCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobCategory);
        }

        // GET: JobCategories/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobCategory = await _context.JobCategories.FindAsync(id);
            if (jobCategory == null)
            {
                return NotFound();
            }
            return View(jobCategory);
        }

        // POST: JobCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobCategoryId,JobCategoryName,IsApproved")] JobCategory jobCategory)
        {
            if (id != jobCategory.JobCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobCategoryExists(jobCategory.JobCategoryId))
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
            return View(jobCategory);
        }

        // GET: JobCategories/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobCategory = await _context.JobCategories
                .FirstOrDefaultAsync(m => m.JobCategoryId == id);
            if (jobCategory == null)
            {
                return NotFound();
            }

            return View(jobCategory);
        }

        // POST: JobCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobCategory = await _context.JobCategories.FindAsync(id);
            if (jobCategory != null)
            {
                _context.JobCategories.Remove(jobCategory);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobCategoryExists(int id)
        {
            return _context.JobCategories.Any(e => e.JobCategoryId == id);
        }
        [HttpPost]
        public IActionResult ApproveJobCategory(int jobCategoryId)
        {
            var jobCategory = _context.JobCategories.Find(jobCategoryId);
            if (jobCategory != null)
            {
                jobCategory.IsApproved = true;
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "JobCategories");
        }
        [HttpPost]
        public IActionResult RejectJobCategory(int jobCategoryId)
        {
            var jobCategory = _context.JobCategories.Find(jobCategoryId);
            if (jobCategory != null)
            {
                _context.JobCategories.Remove(jobCategory);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "JobCategories");
        }
    }
}
