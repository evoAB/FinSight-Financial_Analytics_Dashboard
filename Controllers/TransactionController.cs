using Microsoft.AspNetCore.Mvc;
using FinanceDashboard.Data;
using FinanceDashboard.Models;
using FinanceDashboard.DTOs.Transaction;
using Microsoft.AspNetCore.Authorization;

namespace FinanceDashboard.Controllers;

[Authorize(Policy = "AdminOrAnalyst")]
[ApiController]
[Route("api/transaction")]
public class TransactionController : ControllerBase
{
    private readonly AppDbContext _context;

    public TransactionController(AppDbContext context)
    {
        _context = context;
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult GetAll()
    {
        var transactions = _context.Transactions
            .Select(t => new TransactionResponseDto
            {
                Id = t.Id,
                AccountId = t.AccountId,
                AccountName = t.Account.Name,
                CategoryId = t.CategoryId,
                CategoryName = t.Category.Name,
                Amount = t.Amount,
                Date = t.Date,
                Type = t.Type
            }).ToList();

        return Ok(transactions);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var t = _context.Transactions
            .Where(x => x.Id == id)
            .Select(t => new TransactionResponseDto
            {
                Id = t.Id,
                AccountId = t.AccountId,
                AccountName = t.Account.Name,
                CategoryId = t.CategoryId,
                CategoryName = t.Category.Name,
                Amount = t.Amount,
                Date = t.Date,
                Type = t.Type
            }).FirstOrDefault();

        if (t == null) return NotFound();

        return Ok(t);
    }

    [HttpPost]
    public IActionResult Create(CreateTransactionDto dto)
    {
        var transaction = new Transaction
        {
            AccountId = dto.AccountId,
            Amount = dto.Amount,
            Date = dto.Date,
            CategoryId = dto.CategoryId,
            Type = dto.Type
        };

        _context.Transactions.Add(transaction);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = transaction.Id }, transaction);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateTransactionDto dto)
    {
        var t = _context.Transactions.Find(id);
        if (t == null) return NotFound();

        t.Amount = dto.Amount;
        t.Date = dto.Date;
        t.CategoryId = dto.CategoryId;
        t.Type = dto.Type;

        _context.SaveChanges();

        return NoContent();
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var t = _context.Transactions.Find(id);
        if (t == null) return NotFound();

        _context.Transactions.Remove(t);
        _context.SaveChanges();

        return NoContent();
    }
}