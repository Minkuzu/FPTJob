using System.ComponentModel.DataAnnotations;

namespace FPTJob.Models
{
    public class JobListing
    {
        [Key] 
        public int JobListingId { get; set; }
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }
        [Display(Name = "Job Description")]
        public string JobDescription { get; set; }
        [Display(Name = "Job Requirements")]
        public string JobRequirement { get; set; }
        [Display(Name = "Job Salary")]
        public decimal JobSalary { get; set; }
        [Display(Name = "Job Deadline")]
        public DateTime? DeadLine { get; set; }
        [Display(Name = "Employer Name")]
        public string? EmployerId { get; set; }
        [Display(Name = "Category Name")]
        public int JobCategoryId { get; set; }
        public virtual Employer? Employer { get; set; }
        public virtual JobCategory? JobCategory { get; set; }
        public virtual ICollection<JobApplication>? JobApplications { get; set; }
        //Don't add a new attribute related to relationships or it will ruin
    }
}
