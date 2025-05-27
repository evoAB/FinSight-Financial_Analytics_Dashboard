namespace FinanceDashboard.DTOs.Account;

public class CreateAccountDto
{
    public string Name { get; set; }
    public string AccountNumber { get; set; }
    public double RiskScore { get; set; }
}
