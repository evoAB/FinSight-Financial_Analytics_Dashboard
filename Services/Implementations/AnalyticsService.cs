using FinanceDashboard.Data;
using FinanceDashboard.DTOs.Account;
using FinanceDashboard.DTOs.Analytics;
using Microsoft.EntityFrameworkCore;

namespace FinanceDashboard.Services;

public class AnalyticsService : IAnalyticsService
{
    private readonly AppDbContext _context;

    public AnalyticsService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<AccountResponseDto>> GetTopRiskyAccountsAsync(int count)
    {
        return await _context.Accounts
            .OrderByDescending(a => a.RiskScore)
            .Take(count)
            .Select(a => new AccountResponseDto
            {
                Id = a.Id,
                Name = a.Name,
                AccountNumber = a.AccountNumber,
                RiskScore = a.RiskScore,
                CreatedAt = a.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<List<MonthlySummaryDto>> GetMonthlyExpenseSummaryAsync()
    {
        var transactions = await _context.Transactions.ToListAsync();

        return transactions
                .GroupBy(t => new
                {
                    Month = new DateTime(t.Date.Year, t.Date.Month, 1),
                    t.Type
                })
                .Select(g => new MonthlySummaryDto
                {
                    Month = g.Key.Month.ToString("yyyy-MM"),
                    Type = g.Key.Type,
                    Total = g.Sum(x => x.Amount)
                })
                .OrderBy(x => x.Month)
                .ToList();
    }


    public async Task<List<CategorySummaryDto>> GetCategorySummaryAsync()
    {
        return await _context.Transactions
            .Include(t => t.Category)
            .GroupBy(t => new { t.Category.Name, t.Type })
            .Select(g => new CategorySummaryDto
            {
                Category = g.Key.Name,
                Type = g.Key.Type,
                Total = g.Sum(x => x.Amount)
            })
            .OrderByDescending(x => x.Total)
            .ToListAsync();
    }

}