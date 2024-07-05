using CreditApplicationsTask.Models;

namespace CreditApplicationsTask.Services
{
    // Define an interface to represent the credit decision service
    public interface ICreditDecisionService
    {
        // Method to make a credit decision based on a credit application request.
        // Takes a CreditApplicationRequest object as parameter and returns a CreditApplicationResponse object.
        CreditApplicationResponse MakeCreditDecision(CreditApplicationRequest request);
    }
}
