using System.ComponentModel.DataAnnotations;

namespace FPTJob.Models
{
    public class JobCategory
    {
        [Key]
        public int JobCategoryId { get; set; }
        [Display(Name = "Category Name")]
        public string JobCategoryName { get; set; }
        [Display(Name = "Status")]
        public bool IsApproved { get; set; } = false;
        public virtual ICollection<JobListing>? JobListing { get; set; }
        public string GetStatus()
        {
            return IsApproved ? "Approved" : "Pending";
        }
    }
}
