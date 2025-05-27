namespace FinanceDashboard.DTOs.Transaction;

public class UpdateTransactionDto
{
    public int CategoryId { get; set; }
    public double Amount { get; set; }
    public DateTime Date { get; set; }
    public string Type { get; set; }
}