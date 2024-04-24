using System.ComponentModel.DataAnnotations;

namespace FPTJob.Models
{
    public class EditUserViewModel
    {
            public EditUserViewModel()
            {
                Roles = new List<string>();
            }
            [Required]
            public string Id { get; set; }
            [Required]
            [Display(Name = "Username")]
            public string UserName { get; set; }
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            [Display(Name = "First Name")]
            public string? FirstName { get; set; }
            [Display(Name = "Last Name")]
            public string? LastName { get; set; }
            public string? Address { get; set; }
            public string? Phone { get; set; }
            public string? Skill { get; set; }
            public string? Company {  get; set; }
            public string? ProfilePicture { get; set; }
            public IList<string> Roles { get; set; }
    }
}
