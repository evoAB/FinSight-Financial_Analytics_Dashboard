namespace FinanceDashboard.Models;

public class Transaction
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int AccountId { get; set; }
    public Account Account { get; set; }

    public double Amount { get; set; }
    public DateTime Date { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public string Type { get; set; }
}