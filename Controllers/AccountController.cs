using Microsoft.AspNetCore.Mvc;
using FinanceDashboard.Data;
using FinanceDashboard.Models;
using FinanceDashboard.DTOs.Account;
using FinanceDashboard.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace FinanceDashboard.Controllers;

[Authorize(Policy = "AdminOrAnalyst")]
[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly AppDbContext _context;

    public AccountController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var accounts = _context.Accounts
            .Select(a => new AccountResponseDto
            {
                Id = a.Id,
                Name = a.Name,
                AccountNumber = a.AccountNumber,
                RiskScore = a.RiskScore,
                CreatedAt = a.CreatedAt
            }).ToList();

        return Ok(accounts);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var a = _context.Accounts.Find(id);
        if (a == null) return NotFound();

        var result = new AccountResponseDto
        {
            Id = a.Id,
            Name = a.Name,
            AccountNumber = a.AccountNumber,
            RiskScore = a.RiskScore,
            CreatedAt = a.CreatedAt
        };

        return Ok(result);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPost]
    public IActionResult Create(CreateAccountDto dto)
    {
        var account = new Account
        {
            Name = dto.Name,
            AccountNumber = dto.AccountNumber,
            RiskScore = dto.RiskScore
        };

        _context.Accounts.Add(account);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = account.Id }, account);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateAccountDto dto)
    {
        var account = _context.Accounts.Find(id);
        if (account == null) return NotFound();

        account.Name = dto.Name;
        account.AccountNumber = dto.AccountNumber;
        account.RiskScore = dto.RiskScore;

        _context.SaveChanges();

        return NoContent();
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var account = _context.Accounts.Find(id);
        if (account == null) return NotFound();

        _context.Accounts.Remove(account);
        _context.SaveChanges();

        return NoContent();
    }
}

