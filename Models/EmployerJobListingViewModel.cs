namespace FPTJob.Models
{
    public class EmployerJobListingViewModel
    {
        public int JobId { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string JobRequirement { get; set; }
        public decimal JobSalary { get; set; }
        public DateTime? DeadLine { get; set; }
        public string EmployerId { get; set; }
        public int JobCategoryId { get; set; }
    }
}
