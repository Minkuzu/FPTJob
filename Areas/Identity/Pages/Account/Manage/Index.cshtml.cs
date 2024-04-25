// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using FPTJob.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FPTJob.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
        }


        public string Username { get; set; }


        [TempData]
        public string StatusMessage { get; set; }


        [BindProperty]
        public InputModel Input { get; set; }


        public class InputModel
        {
            [Display(Name = "First Name")]
            public string FirstName { get; set; }
            [Display(Name = "Last Name")]
            public string LastName { get; set; }
            [Display(Name = "Address")]
            public string Address { get; set; }
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            [Display(Name = "Company")]
            public string Company { get; set; }
            [Display(Name = "Skill")]
            public string Skill { get; set; }
            [Display(Name = "Phone")]
            public string Phone { get; set; }
            public string? ProfilePicture { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            if (User.IsInRole("JobSeeker"))
            {
                var userjobseeker = await _userManager.GetUserAsync(User);
                var jobSeeker = (JobSeeker)userjobseeker;
                var userName = await _userManager.GetUserNameAsync(user);
                var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
                var address = user.Address;
                var firstName = user.FirstName;
                var lastName = user.LastName;
                var phone = user.Phone;
                var skill = jobSeeker.Skill;
                var profilePicture = user.ProfilePicture;

                Username = userName;

                Input = new InputModel
                {
                    PhoneNumber = phoneNumber,
                    Phone = phone,
                    FirstName = firstName,
                    LastName = lastName,
                    Address = address,
                    Skill = skill,
                    ProfilePicture = profilePicture
                };
            }   
            else if (User.IsInRole("Employer"))
            {
                var useremployer = await _userManager.GetUserAsync(User);
                var employer = (Employer)useremployer;
                var userName = await _userManager.GetUserNameAsync(user);
                var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
                var address = user.Address;
                var firstName = user.FirstName;
                var lastName = user.LastName;
                var phone = user.Phone;
                var company = employer.Company;
                var profilePicture = user.ProfilePicture;

                Username = userName;

                Input = new InputModel
                {
                    PhoneNumber = phoneNumber,
                    Phone = phone,
                    FirstName = firstName,
                    LastName = lastName,
                    Address = address,
                    Company = company,
                    ProfilePicture = profilePicture
                };
            }
            
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var firstName = user.FirstName;
            var lastName = user.LastName;
            var address = user.Address;
            var phone = user.Phone;
            var profilePicture = user.ProfilePicture;
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }
            if (Input.FirstName != firstName)
            {
                user.FirstName = Input.FirstName;
                await _userManager.UpdateAsync(user);
            }
            if (Input.LastName != lastName)
            {
                user.LastName = Input.LastName;
                await _userManager.UpdateAsync(user);
            }
            if (Input.Address != address)
            {
                user.Address = Input.Address;
                await _userManager.UpdateAsync(user);
            }
            if (Input.Phone != phone)
            {
                user.Phone = Input.Phone;
                await _userManager.UpdateAsync(user);
            }
            if (Input.ProfilePicture != profilePicture)
            {
                user.ProfilePicture = Input.ProfilePicture;
                await _userManager.UpdateAsync(user);
            }
            if (User.IsInRole("JobSeeker"))
            {
                var jobSeeker = (JobSeeker)user;
                jobSeeker.Skill = Input.Skill; 
                await _userManager.UpdateAsync(user);
            }
            else if (User.IsInRole("Employer"))
            {
                var employer = (Employer)user;
                employer.Company = Input.Company; 
                await _userManager.UpdateAsync(user);
            }
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
