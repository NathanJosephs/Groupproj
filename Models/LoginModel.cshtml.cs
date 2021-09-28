using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Groupproj.Models;
using Microsoft.EntityFrameworkCore;

namespace Groupproj.Views.Accounts
{
    public class LoginModel : PageModel
    {
        private readonly DbContext Db;

        public LoginModel(DbContext Db)
        { 
            this.Db = Db;
        }
        public string ReturnUrl { get; set; }
        [BindProperty]
        [Required]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public async Task<IActionResult> OnPostAsync(string returnUrl)
        {
            returnUrl = Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = Db.RegisterModel.FirstOrDefault(login => login.EmailAddress == EmailAddress && login.Password == Password);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Error: Incorrect Email and/or Password");
                    return Page();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.EmailAddress)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    new AuthenticationProperties { IsPersistent = true });



                return LocalRedirect(returnUrl);
            }

            return Page();
        }
        

    public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            return RedirectToAction("/Index");
    }
}
    }
