namespace FinanceDashboard.DTOs.Analytics;

public class CategorySummaryDto
{
    public string Category { get; set; }
    public string Type { get; set; } // Credit / Debit
    public double Total { get; set; }
}