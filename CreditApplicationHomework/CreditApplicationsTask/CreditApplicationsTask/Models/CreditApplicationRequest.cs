using System.ComponentModel.DataAnnotations;

namespace CreditApplicationsTask.Models
{
    public class CreditApplicationRequest
    {
        [Range(2000, 69000, ErrorMessage = "Credit amount must be a number between 2000 and 69000")]
        public decimal CreditAmount { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Term must be a positive number")]
        public int Term { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Existing credit amount must be positive")]
        public decimal ExistingCreditAmount { get; set; }
    }
}
