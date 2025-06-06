using FinanceDashboard.Data;
using FinanceDashboard.DTOs;
using FinanceDashboard.DTOs.Transaction;
using FinanceDashboard.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceDashboard.Services;

public class TransactionService(AppDbContext context) : ITransactionService
{
    public async Task<IEnumerable<TransactionResponseDto>> GetAllAsync()
    {
        return await context.Transactions.Select(transaction => new TransactionResponseDto
        {
            Id = transaction.Id,
            Title = transaction.Title,
            AccountId = transaction.AccountId,
            AccountName = transaction.Account.Name,
            CategoryId = transaction.CategoryId,
            CategoryName = transaction.Category.Name,
            Amount = transaction.Amount,
            Date = transaction.Date,
            Type = transaction.Type
        }).ToListAsync();
    }

    public async Task<TransactionResponseDto> GetByIdAsync(int id)
    {
        var transaction = await context.Transactions.FindAsync(id);
        if (transaction == null) return null;

        return new TransactionResponseDto
        {
            Id = transaction.Id,
            Title = transaction.Title,
            AccountId = transaction.AccountId,
            AccountName = transaction.Account.Name,
            CategoryId = transaction.CategoryId,
            CategoryName = transaction.Category.Name,
            Amount = transaction.Amount,
            Date = transaction.Date,
            Type = transaction.Type
        };
    }

    public async Task<TransactionResponseDto> CreateAsync(CreateTransactionDto dto)
    {
        using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            var transactionEntity = new Transaction
            {
                AccountId = dto.AccountId,
                Title = dto.Title,
                Amount = dto.Amount,
                Date = DateTime.SpecifyKind(dto.Date, DateTimeKind.Utc),
                CategoryId = dto.CategoryId,
                Type = dto.Type
            };

            context.Transactions.Add(transactionEntity);
            await context.SaveChangesAsync();

            await transaction.CommitAsync();

            return new TransactionResponseDto
            {
                Id = transactionEntity.Id,
                Title = transactionEntity.Title,
                AccountId = transactionEntity.AccountId,
                AccountName = transactionEntity.Account?.Name,
                CategoryId = transactionEntity.CategoryId,
                CategoryName = transactionEntity.Category?.Name,
                Amount = transactionEntity.Amount,
                Date = transactionEntity.Date,
                Type = transactionEntity.Type
            };
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }


    public async Task<TransactionResponseDto?> UpdateAsync(int id, UpdateTransactionDto dto)
    {
        using var dbTransaction = await context.Database.BeginTransactionAsync();

        try
        {
            var transaction = await context.Transactions
                    .Include(t => t.Account)
                    .Include(t => t.Category)
                    .FirstOrDefaultAsync(t => t.Id == id);

            if (transaction == null) return null;

            transaction.Title = dto.Title;
            transaction.Amount = dto.Amount;
            transaction.Date = DateTime.SpecifyKind(dto.Date, DateTimeKind.Utc);
            transaction.CategoryId = dto.CategoryId;
            transaction.Type = dto.Type;

            await context.SaveChangesAsync();
            await dbTransaction.CommitAsync();

            return new TransactionResponseDto
            {
                Id = transaction.Id,
                Title = transaction.Title,
                AccountId = transaction.AccountId,
                AccountName = transaction.Account?.Name,
                CategoryId = transaction.CategoryId,
                CategoryName = transaction.Category?.Name,
                Amount = transaction.Amount,
                Date = transaction.Date,
                Type = transaction.Type
            };
        }
        catch
        {
            await dbTransaction.RollbackAsync();
            throw;
        }
    }

    public async Task DeleteAsync(int id)
    {
        var transaction = await context.Transactions.FindAsync(id);
        if (transaction == null) return;

        context.Transactions.Remove(transaction);
        await context.SaveChangesAsync();
    }
}