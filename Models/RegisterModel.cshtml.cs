using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Groupproj.Models;

namespace Groupproj.Views.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly unidbContext Db;

        public RegisterModel(unidbContext Db)
        {
            this.Db = Db;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "First Name")]
            public string Fname { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Last Name")]
            public string LName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(20, ErrorMessage = "Password must be at least 8 characters long.", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "Password matching error.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = Db.RegisterModel.Where(f => f.EmailAddress == Input.Email).FirstOrDefault();
                if (user != null)
                {
                    ModelState.AddModelError(string.Empty, Input.Email + "In Use");
                }
                else
                {
                    user = new Models.RegisterModel() { EmailAddress = Input.Email, Password = Input.Password };
                    Db.RegisterModel.Add(user);
                    await Db.SaveChangesAsync();
                    return RedirectToPage("Confirmed!", new { email = Input.Email });
                }

            }

            return Page();
        }
    }
}