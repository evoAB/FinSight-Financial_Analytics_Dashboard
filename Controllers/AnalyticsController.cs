using FinanceDashboard.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceDashboard.Controllers;

[Authorize(Policy = "AdminOrAnalyst")]
[ApiController]
[Route("api/[controller]")]
public class AnalyticsController(IAnalyticsService analyticsService) : ControllerBase
{

    [HttpGet("top-risky-accounts")]
    public async Task<IActionResult> GetTopRiskyAccounts([FromQuery] int count = 5)
    {
        var result = await analyticsService.GetTopRiskyAccountsAsync(count);
        return Ok(result);
    }

    [HttpGet("monthly-expense-summary")]
    public async Task<IActionResult> GetMonthlyExpenseSummary()
    {
        var result = await analyticsService.GetMonthlyExpenseSummaryAsync();
        return Ok(result);
    }

    [HttpGet("category-summary")]
    public async Task<IActionResult> GetCategorySummary()
    {
        var result = await analyticsService.GetCategorySummaryAsync();
        return Ok(result);
    }

}