using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using VehicleInsuranceProject.BusinessLogic;
using VehicleInsuranceProject.Models;
using VehicleInsuranceProject.Repository;

namespace VehicleInsuranceProject.Controllers
{
    public class HomeController : Controller

    {
        private readonly ClaimDbContext _context;
        private readonly IClaim _claim;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ClaimDbContext context, IClaim claim)
        {
            _logger = logger;
            _context = context;
            _claim = claim;


        }

        public IActionResult ClaimForm(int? id)
        {
            if (id == null)
            {
                var claimsInDb = _context.Claims.SingleOrDefault(claims => claims.claimId == id);
                return View(claimsInDb);
            }
            return View();

        }

        public IActionResult AddClaim(ClaimTable model)
        {
            _claim.setClaimDetails(model);

            return RedirectToAction("ClaimDetails");
        }

        public IActionResult ClaimDetails()
        {
            var claimsInDb = _context.Claims.ToList();
            return View(claimsInDb);
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
