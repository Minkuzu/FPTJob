namespace FPTJob.Models
{
    public class Employer : ApplicationUser
    {
        public string? Company { get; set; }
        public virtual ICollection<JobListing>? Jobs { get; set; }
    }
}
