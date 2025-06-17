using System.ComponentModel.DataAnnotations;

namespace VehicleInsuranceProject.ViewModels
{
    public class CustomerLoginViewModel
    {
        [Required(ErrorMessage = "Username or Email is required.")]
        [Display(Name = "Username / Email")]
        public string Name { get; set; } // This will be used for both username or email

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        
    }
}
