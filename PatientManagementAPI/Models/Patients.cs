using System.ComponentModel.DataAnnotations;

namespace PatientManagementAPI.Models
{
    public class Patients
    {
        public int PatientID { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string? LastName { get; set; }

        public string? FullName { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        public string? Age { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string? PatientAddress { get; set; }

        [Required(ErrorMessage = "Contact number is required.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string? EmailAddress { get; set; }
    }
}
