using CreditApplicationsTask.Models;
using System.ComponentModel.DataAnnotations;

namespace CreditApplicationsTask.Services
{
    public class CreditDecisionService : ICreditDecisionService
    {
        // Main method to make a credit decision based on the input request
        public CreditApplicationResponse MakeCreditDecision(CreditApplicationRequest request)
        {
            // Validate the request using the custom ValidateRequest method
            ValidateRequest(request);

            // Calculate the total future debt by adding the requested credit amount to existing credit amount.
            var totalFutureDebt = request.CreditAmount + request.ExistingCreditAmount;
            var decision = "Yes";
            decimal interestRate;
            
            // Decision logic based on the term of the credit application
            if (request.Term < 6)
            {
                if (request.CreditAmount < 2000 || request.CreditAmount > 69000)
                {
                    decision = "No";
                }
                else if (request.CreditAmount >= 2000 || request.CreditAmount <= 69000)
                {
                    decision = "Yes";
                }
            }
            else
            {
                if (request.CreditAmount < 2000 || request.CreditAmount > 69000)
                {
                    decision = "No";
                }
                else if (request.CreditAmount >= 2000 || request.CreditAmount <= 69000)
                {
                    decision = "Yes";
                }
            }
            
            // Determine the interest rate based on the term length
            if (request.Term <= 12)
            {
                interestRate = CalculateInterestRateForShortTerm(totalFutureDebt);
            }
            else
            {
                interestRate = CalculateInterestRateForLongTerm(totalFutureDebt);
            }
            
            // Return the decision and interest rate in a CreditApplicationResponse object
            return new CreditApplicationResponse { Decision = decision, InterestRate = interestRate };
        }
        
        // Method to calculate the interest rate for short-term loans
        private decimal CalculateInterestRateForShortTerm(decimal totalFutureDebt)
        {
            if (totalFutureDebt < 20000)
            {
                return 3m;
            }
            else if (totalFutureDebt >= 20000 && totalFutureDebt <= 39000)
            {
                return 4m;
            }
            else if (totalFutureDebt >= 40000  && totalFutureDebt <= 59000)
            {
                return 5m;
            }
            else
            {
                return 6m;
            }
        }
        
        // Method to calculate the interest rate for long-term loans
        private decimal CalculateInterestRateForLongTerm(decimal totalFutureDebt)
        {
            var baseRate = CalculateInterestRateForShortTerm(totalFutureDebt);
            return baseRate + 1m;
        }
        
        // Method to validate the credit application request
        private void ValidateRequest(CreditApplicationRequest request)
        {
            var validationContext = new ValidationContext(request, null, null);
            Validator.ValidateObject(request, validationContext, validateAllProperties: true);
        }
    }
}
