using CreditApplicationsTask.Models;

namespace CreditApplicationsTask.Services
{
    public interface ICreditDecisionService
    {
        CreditApplicationResponse MakeCreditDecision(CreditApplicationRequest request);
    }
}
