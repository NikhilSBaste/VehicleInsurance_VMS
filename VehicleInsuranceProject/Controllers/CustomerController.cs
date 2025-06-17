using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using VehicleInsuranceProject.BusinessLogic.Services;
using VehicleInsuranceProject.Repository.Models;
using VehicleInsuranceProject.ViewModels;
using System.Reflection.Metadata.Ecma335;

namespace VehicleInsuranceProject.Controllers
{
    [Authorize] // Require authentication for all actions in this controller
    public class CustomerController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ICustomerService _customerService;
        private readonly UserManager<IdentityUser> _userManager;

        public CustomerController(ICustomerService customerService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _customerService = customerService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(CustomerLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Name);

                if (user != null)
                {
                    // Call PasswordSignInAsync with isPersistent: false (no "Remember me")
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt. Please check your username/email and password.");
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt. Please check your username/email and password.");
                    return View(model);
                }
            }
            return View(model);
        }

       



        [AllowAnonymous] // Override Authorize attribute for Register
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(CustomerRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer
                {
                    Name = model.Name,
                    Email = model.Email,
                    Phone = model.Phone,
                    Address = model.Address
                };

                var result = await _customerService.CreateCustomerAsync(customer, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home"); // Redirect on successful registration
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> UpdateProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            var customer = await _customerService.GetCustomerByIdAsync(user.Id);
            if (customer == null)
            {
                return NotFound();
            }
            var model = new CustomerUpdateViewModel
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(CustomerUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var customer = await _customerService.GetCustomerByIdAsync(user.Id);
                if (customer == null)
                {
                    return NotFound();
                }
                customer.Name = model.Name;
                customer.Email = model.Email;
                customer.Phone = model.Phone;
                customer.Address = model.Address;
                await _customerService.UpdateCustomerAsync(customer);
                return RedirectToAction("Index", "Home"); // Redirect after update
            }
            return View(model);
        }


        public async Task<IActionResult> Details()
        {
            var user = await _userManager.GetUserAsync(User);
            var customer = await _customerService.GetCustomerByIdAsync(user.Id);
            if (customer == null)
            {
                return NotFound();
            }
            var model = new CustomerDetailsViewModel
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address
            };
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
    } 
}