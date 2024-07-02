using Xunit;
using CreditApplicationsTask.Models;
using CreditApplicationsTask.Services;
using System.ComponentModel.DataAnnotations;

namespace CreditApplicationsTask.Tests
{
    public class CreditApplicationsTaskTests
    {
        private readonly CreditDecisionService _service = new CreditDecisionService();

        [Theory]
        [InlineData(1000, 5, 0, "No", 3)]          // term < 6 months, amount within range, low total debt
        [InlineData(1000, 5, 40000, "No", 6)]      // term < 6 months, high total debt
        [InlineData(50000, 5, 0, "Yes", 3)]         // term < 6 months, max in range
        [InlineData(50001, 5, 0, "No", 0)]          // term < 6 months, above range
        [InlineData(2000, 12, 0, "Yes", 3)]         // term >= 6, amount within range, low total debt
        [InlineData(2000, 13, 0, "Yes", 4)]         // term > 12, increased interest rate
        [InlineData(70000, 12, 0, "No", 0)]         // term >= 6, amount above range
        [InlineData(5000, 12, 10000, "Yes", 3)]     // Applied amount within range, low total future debt
        [InlineData(5000, 20, 20000, "Yes", 5)]     // Applied amount within range, increased interest (12+1) due to long term
        [InlineData(5000, 12, 30000, "Yes", 5)]     // Applied amount within range, mid total future debt
        [InlineData(70000, 12, 10000, "No", 0)]     // Greater than 69000 applied amount
        [InlineData(5000, 12, 25000, "Yes", 4)]     // Applied amount within range, high total future debt
        public void MakeCreditDecision_ReturnExpectedResults(decimal creditAmount, int term, decimal existingCredit, string expectedDecision, decimal expectedRate)
        {
            var request = new CreditApplicationRequest
            {
                CreditAmount = creditAmount,
                Term = term,
                ExistingCreditAmount = existingCredit,
            };

            var response = _service.MakeCreditDecision(request);

            Assert.Equal(expectedDecision, response.Decision);
            Assert.Equal(expectedRate, response.InterestRate);
        }

        [Theory]
        [InlineData(1500, 12, 0)]       //Invalid credit amount
        [InlineData(70000, 12, 0)]      //Invalid credit amount
        [InlineData(5000, -1, 0)]       //Invalid term
        [InlineData(5000, 12, -1)]      //InvalidExistingCreditAmount
        [InlineData(5000, 12, 2)]       //Valid data
        public void MakeCreditDecision_ThrowsValidationException_ForInvalidInputs(decimal creditAmount, int term, decimal existingCredit)
        {
            var request = new CreditApplicationRequest
            {
                CreditAmount = creditAmount,
                Term = term,
                ExistingCreditAmount = existingCredit
            };

            Assert.Throws<ValidationException>(() => _service.MakeCreditDecision(request));
        }
    }
}
