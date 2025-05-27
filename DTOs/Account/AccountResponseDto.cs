namespace FinanceDashboard.DTOs.Account;

public class AccountResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string AccountNumber { get; set; }
    public double RiskScore { get; set; }
    public DateTime CreatedAt { get; set; }
}