using System.ComponentModel.DataAnnotations;

namespace FPTJob.Models
{
    public class JobApplicationViewModel
    {
        public int JobApplicationId { get; set; }
        public string Resume { get; set; }
        public string Status { get; set; }
        [Display(Name = "Job")]
        public int JobId { get; set; }

        [Display(Name = "Job Seeker Name")]
        public string JobSeekerId { get; set; }
        [Display(Name = "Job Seeker Name")]
        public virtual JobSeeker? JobSeeker { get; set; }
        [Display(Name = "Job")]
        public virtual JobListing? JobListing { get; set; }
    }
}
