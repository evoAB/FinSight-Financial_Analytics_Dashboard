namespace FinanceDashboard.DTOs.Analytics;

public class MonthlySummaryDto
{
    public string Month { get; set; } // Format: YYYY-MM
    public string Type { get; set; }  // Credit / Debit
    public double Total { get; set; }
}
