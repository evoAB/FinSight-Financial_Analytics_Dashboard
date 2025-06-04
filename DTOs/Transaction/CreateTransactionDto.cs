namespace FinanceDashboard.DTOs.Transaction;

public class CreateTransactionDto
{
    public int AccountId { get; set; }
    public string Title { get; set; }
    public int CategoryId { get; set; }
    public double Amount { get; set; }
    public DateTime Date { get; set; }
    public string Type { get; set; }
}