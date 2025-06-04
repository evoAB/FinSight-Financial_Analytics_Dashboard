namespace FinanceDashboard.DTOs.Transaction;

public class TransactionResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int AccountId { get; set; }
    public string AccountName { get; set; }

    public int CategoryId { get; set; }
    public string CategoryName { get; set; }

    public double Amount { get; set; }
    public DateTime Date { get; set; }
    public string Type { get; set; }
}