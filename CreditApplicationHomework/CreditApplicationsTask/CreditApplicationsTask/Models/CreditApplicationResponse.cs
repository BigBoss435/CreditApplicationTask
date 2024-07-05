namespace CreditApplicationsTask.Models
{
    // Class representing the response for a credit application, DTO
    public class CreditApplicationResponse
    {
        // Property representing the decision made on the credit application (e.g., "Approved" ir "Denied")
        public string Decision { get; set; }
        // Property representing the interest rate applicable to the credit application
        public decimal InterestRate { get; set; }
    }
}
