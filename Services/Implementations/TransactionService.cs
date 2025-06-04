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
        var transaction = new Transaction
        {
            AccountId = dto.AccountId,
            Title = dto.Title,
            Amount = dto.Amount,
            Date = dto.Date,
            CategoryId = dto.CategoryId,
            Type = dto.Type
        };

        context.Transactions.Add(transaction);
        context.SaveChanges();

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

    public async Task<TransactionResponseDto?> UpdateAsync(int id, UpdateTransactionDto dto)
    {
        var transaction = await context.Transactions.FindAsync(id);
        if (transaction == null) return null;

        transaction.Title = dto.Title;
        transaction.Amount = dto.Amount;
        transaction.Date = dto.Date;
        transaction.CategoryId = dto.CategoryId;
        transaction.Type = dto.Type;

        context.SaveChanges();

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

    public async Task DeleteAsync(int id)
    {
        var transaction = await context.Transactions.FindAsync(id);
        if (transaction == null) return;

        context.Transactions.Remove(transaction);
        context.SaveChanges();

    }
}