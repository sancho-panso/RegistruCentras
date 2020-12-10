using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RegistruCentras.Domains;
using RegistruCentras.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistruCentras.Web.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegistrationController(UserManager<AppUser> userManager, 
                                      SignInManager<AppUser> signInManager, 
                                      IConfiguration configuration,
                                      RoleManager<IdentityRole> role
                                      )
                                      
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = role;
        }
        public IActionResult Index()
        {

            return View();
        } 

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login(string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(SignInViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.AuthError = "Bad input data";
                return View("~/Views/Registration/Login.cshtml", model);
            }

            var result =await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public async Task<IActionResult> Register(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser newUser = new AppUser()
                {
                    Email = model.Email,
                    UserName = model.Email
                };

                var result = await _userManager.CreateAsync(newUser, model.ConfirmPassword);
                if (result.Succeeded)
                {
                    bool isAdmin = EmailCheckForAdmin(model.Email);
                    if (isAdmin)
                    {
                        await _userManager.AddToRoleAsync(newUser, "Admin");
                    }
                    return RedirectToAction("Login");//redirect to logging
                }
                
            }
            return View("~/Views/Registration/Index.cshtml", model);
        }

        private bool EmailCheckForAdmin(string email)
        {
            return email.EndsWith(_configuration["ADMIN"]);
        }
    }
}
