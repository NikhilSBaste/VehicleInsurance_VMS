using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace VehicleInsuranceProject.Repository.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string IdentityUserId { get; set; } // Foreign Key to AspNetUsers.Id
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public IdentityUser IdentityUser { get; set; } // Navigation property (optional)
    }
}
