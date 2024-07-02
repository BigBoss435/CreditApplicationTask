using CreditApplicationsTask.Models;
using System.ComponentModel.DataAnnotations;

namespace CreditApplicationsTask.Services
{
    public class CreditDecisionService : ICreditDecisionService
    {
        public CreditApplicationResponse MakeCreditDecision(CreditApplicationRequest request)
        {
            ValidateRequest(request);

            var totalFutureDebt = request.CreditAmount + request.ExistingCreditAmount;
            var decision = "Yes";
            decimal interestRate;

            if (request.Term < 6)
            {
                if (request.CreditAmount < 2000 || request.CreditAmount > 69000)
                {
                    decision = "No";
                }
                else if (request.CreditAmount > 2000 || request.CreditAmount < 69000)
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
                else if (request.CreditAmount > 2000 || request.CreditAmount < 69000)
                {
                    decision = "Yes";
                }
            }

            if (request.Term <= 12)
            {
                interestRate = CalculateInterestRateForShortTerm(totalFutureDebt);
            }
            else
            {
                interestRate = CalculateInterestRateForLongTerm(totalFutureDebt);
            }

            return new CreditApplicationResponse { Decision = decision, InterestRate = interestRate };
        }

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

        private decimal CalculateInterestRateForLongTerm(decimal totalFutureDebt)
        {
            var baseRate = CalculateInterestRateForShortTerm(totalFutureDebt);
            return baseRate + 1m;
        }

        private void ValidateRequest(CreditApplicationRequest request)
        {
            var validationContext = new ValidationContext(request, null, null);
            Validator.ValidateObject(request, validationContext, validateAllProperties: true);
        }
    }
}
