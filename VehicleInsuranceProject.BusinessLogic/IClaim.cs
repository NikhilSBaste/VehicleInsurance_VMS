using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInsuranceProject.Repository;

namespace VehicleInsuranceProject.BusinessLogic
{
    public interface IClaim
    {
        void setClaimDetails(ClaimTable claimDetail);
    }
}
