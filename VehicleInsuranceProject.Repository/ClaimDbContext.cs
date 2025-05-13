using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VehicleInsuranceProject.Repository
{
    public class ClaimDbContext : DbContext
    {
        public ClaimDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<ClaimTable> Claims { get; set; }
    }
}
