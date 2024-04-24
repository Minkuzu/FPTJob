using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FPTJob.Models
{
    public class JobApplication
    {
        [Key]
        public int JobApplicationId { get; set; }
        public string Resume { get; set; }
        public string Status { get; set; }
        [ForeignKey("JobListing")]
        [Display(Name = "Job")]
        public int JobListingId { get; set; }
        [Display(Name = "Job Seeker Name")]
        public string JobSeekerId { get; set; }
        [Display(Name = "Job Seeker Name")]
        public virtual JobSeeker? JobSeeker { get; set; }
        public virtual JobListing? JobListing { get; set; }
        public JobApplication() 
        {
            Status = "Applied";
        }
    }
}
