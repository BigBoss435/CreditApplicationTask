using Microsoft.AspNetCore.Mvc;
using CreditApplicationsTask.Models;
using CreditApplicationsTask.Services;
using System.ComponentModel.DataAnnotations;

namespace CreditApplicationsTask.Controllers
{
    // Route attribute specifies that the controller will handle requests at the "api/creditdecision" route
    [Route("api/[controller]")]
    [ApiController]
    public class CreditDecisionController : ControllerBase
    {
        // Dependency injection for the credit decision service
        private readonly ICreditDecisionService _creditDecisionService;
        
        // Constructor for the controller that takes an ICreditDecisionService
        // and assigns it to the _creditDecisionService member
        public CreditDecisionController(ICreditDecisionService creditDecisionService)
        {
            _creditDecisionService = creditDecisionService;
        }
        
        // Endpoint for processing credit decision. It receives a CreditApplicationRequest in the request body.
        // The [FromBody] attribute tells ASP.NET Core to bind the incoming request body to the parameter
        [HttpPost]
        public IActionResult GetCreditDecision([FromBody] CreditApplicationRequest request)
        {
            // Check if the model state is valid.
            // If not, return a 400 Bad Request response with the model's validation errors
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Use the credit decision service to make a credit decision based on the request.
                var response = _creditDecisionService.MakeCreditDecision(request);
                // Return 200 OK response with the decision response
                return Ok(response);
            }
            catch (ValidationException ex)
            {
                // If a validation exception occurs, return a 400 Bad Request response with the error message.
                return BadRequest(new { error = ex.Message });
            }

        }
    }
}
