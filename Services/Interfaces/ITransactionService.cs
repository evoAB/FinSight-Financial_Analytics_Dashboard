using System.Transactions;
using FinanceDashboard.DTOs.Transaction;

public interface ITransactionService
{
    Task<IEnumerable<TransactionResponseDto>> GetAllAsync();
    Task<TransactionResponseDto> GetByIdAsync(int id);
    Task<TransactionResponseDto> CreateAsync(CreateTransactionDto dto);
    Task<TransactionResponseDto> UpdateAsync(int id, UpdateTransactionDto dto);
    Task DeleteAsync(int id);
}
