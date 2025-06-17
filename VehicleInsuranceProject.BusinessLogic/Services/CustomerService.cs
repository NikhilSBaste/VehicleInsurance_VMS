using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VehicleInsuranceProject.Repository.Data;
using VehicleInsuranceProject.Repository.Models;

namespace VehicleInsuranceProject.BusinessLogic.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public CustomerService(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IdentityResult> CreateCustomerAsync(Customer customer, string password)
        {
            var user = new IdentityUser { UserName = customer.Email, Email = customer.Email };
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                customer.IdentityUserId = user.Id;
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
            }

            return result;
        }

        public async Task<Customer> GetCustomerByIdAsync(string userId)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.IdentityUserId == userId);
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            return true; //Or return appropriate boolean based on result
        }
    }
}
