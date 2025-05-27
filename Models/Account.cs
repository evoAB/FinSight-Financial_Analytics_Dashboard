namespace FinanceDashboard.Models;

public class Account
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string AccountNumber { get; set; }
    public double RiskScore { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}