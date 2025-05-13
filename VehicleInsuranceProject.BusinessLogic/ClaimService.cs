using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInsuranceProject.Repository;

namespace VehicleInsuranceProject.BusinessLogic
{
    public class ClaimService : IClaim
    {
        private readonly ClaimDbContext _context;

        public ClaimService(ClaimDbContext context)
        {
            _context = context;
        }

        public void setClaimDetails(ClaimTable claimDetail)
        {
            var claimDetails = _context.Add(claimDetail);
            _context.SaveChanges();
        }
    }
}
