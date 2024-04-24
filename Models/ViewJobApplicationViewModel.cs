namespace FPTJob.Models
{
    public class ViewJobApplicationViewModel
    {
        public JobListing JobListing { get; set; }
        public ICollection<JobApplication> JobApplications { get; set; }
    }
}
