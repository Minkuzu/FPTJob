using Microsoft.AspNetCore.Identity;

namespace FPTJob.Models
{
    public class JobSeeker : ApplicationUser
    {
        public string? Skill { get; set; }
        public virtual ICollection<JobApplication>? JobApplications { get; set; }
    }
}
