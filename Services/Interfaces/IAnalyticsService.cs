using FinanceDashboard.DTOs.Account;
using FinanceDashboard.DTOs.Analytics;

namespace FinanceDashboard.Services;

public interface IAnalyticsService
{
    Task<List<AccountResponseDto>> GetTopRiskyAccountsAsync(int count);
    Task<List<MonthlySummaryDto>> GetMonthlyExpenseSummaryAsync();
    Task<List<CategorySummaryDto>> GetCategorySummaryAsync();
}