using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInsuranceProject.Repository
{
    public class ClaimTable
    {
        [Key]
        public int claimId { get; set; }
        public int claimAmount { get; set; }

        [AllowNull]
        public string? claimReason { get; set; }
        public DateOnly claimDate { get; set; }
        public string? claimStatus { get; set; }
    }
}
