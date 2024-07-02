using Microsoft.AspNetCore.Mvc;
using CreditApplicationsTask.Models;
using CreditApplicationsTask.Services;
using System.ComponentModel.DataAnnotations;

namespace CreditApplicationsTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditDecisionController : ControllerBase
    {
        private readonly ICreditDecisionService _creditDecisionService;

        public CreditDecisionController(ICreditDecisionService creditDecisionService)
        {
            _creditDecisionService = creditDecisionService;
        }

        [HttpPost]
        public IActionResult GetCreditDecision([FromBody] CreditApplicationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = _creditDecisionService.MakeCreditDecision(request);
                return Ok(response);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }

        }
    }
}
