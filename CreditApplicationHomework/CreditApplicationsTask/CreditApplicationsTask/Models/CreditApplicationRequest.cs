using System.ComponentModel.DataAnnotations;

namespace CreditApplicationsTask.Models
{
    // Data transfer object, data limits enhance robustness and reliability of the application
    public class CreditApplicationRequest
    {
        // Validation attribute that ensures the credit amount is between 2000 and 69000 inclusive.
        // If the input value is outside this range, an error message will be generated.
        [Range(2000, 69000, ErrorMessage = "Credit amount must be a number between 2000 and 69000")]
        public decimal CreditAmount { get; set; }
        
        // Validation attribute that ensures the term is a positive number greater than or equal to 1.
        // If the value is less than 1, an error message will be generated.
        [Range(1, int.MaxValue, ErrorMessage = "Term must be a positive number")]
        public int Term { get; set; }
        
        // Validation attribute that ensures the existing credit amount is a positive number.
        // If the value is negative, an error message will be generated
        [Range(0, double.MaxValue, ErrorMessage = "Existing credit amount must be positive")]
        public decimal ExistingCreditAmount { get; set; }
    }
}
