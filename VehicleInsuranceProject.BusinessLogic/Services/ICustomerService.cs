using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using VehicleInsuranceProject.Repository.Models;

namespace VehicleInsuranceProject.BusinessLogic.Services
{
    public interface ICustomerService
    {
        Task<IdentityResult> CreateCustomerAsync(Customer customer, string password);
        Task<Customer> GetCustomerByIdAsync(string userId);
        Task<bool> UpdateCustomerAsync(Customer customer);
    }
}
