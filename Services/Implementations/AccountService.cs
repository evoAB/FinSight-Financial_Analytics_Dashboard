using FinanceDashboard.Data;
using FinanceDashboard.DTOs;
using FinanceDashboard.DTOs.Account;
using FinanceDashboard.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceDashboard.Services;

public class AccountService(AppDbContext context) : IAccountService
{
    public async Task<IEnumerable<AccountResponseDto>> GetAllAsync()
    {
        return await context.Accounts.Select(a => new AccountResponseDto
        {
            Id = a.Id,
            Name = a.Name,
            AccountNumber = a.AccountNumber,
            RiskScore = a.RiskScore,
            CreatedAt = a.CreatedAt
        }).ToListAsync();
    }

    public async Task<AccountResponseDto> GetByIdAsync(int id)
    {
        var account = await context.Accounts.FindAsync(id);
        if (account == null) return null;

        return new AccountResponseDto
        {
            Id = account.Id,
            Name = account.Name,
            AccountNumber = account.AccountNumber,
            RiskScore = account.RiskScore,
            CreatedAt = account.CreatedAt
        };
    }

    public async Task<AccountResponseDto> CreateAsync(CreateAccountDto dto)
    {
        var account = new Account
        {
            Name = dto.Name,
            AccountNumber = dto.AccountNumber,
            RiskScore = dto.RiskScore
        };

        context.Accounts.Add(account);
        context.SaveChanges();

        return new AccountResponseDto
        {
            Id = account.Id,
            Name = account.Name,
            AccountNumber = account.AccountNumber,
            RiskScore = account.RiskScore,
            CreatedAt = account.CreatedAt
        };
    }

    public async Task<AccountResponseDto?> UpdateAsync(int id, UpdateAccountDto dto)
    {
        var account = await context.Accounts.FindAsync(id);
        if (account == null) return null;

        account.Name = dto.Name;
        account.AccountNumber = dto.AccountNumber;
        account.RiskScore = dto.RiskScore;

        context.SaveChanges();

        return new AccountResponseDto
        {
            Id = account.Id,
            Name = account.Name,
            AccountNumber = account.AccountNumber,
            RiskScore = account.RiskScore,
            CreatedAt = account.CreatedAt
        };
    }

    public async Task DeleteAsync(int id)
    {
        var account = await context.Accounts.FindAsync(id);
        if (account == null)
            return;
        context.Accounts.Remove(account);
        context.SaveChanges();
    }
}