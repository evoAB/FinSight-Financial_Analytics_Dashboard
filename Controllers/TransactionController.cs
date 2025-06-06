using Microsoft.AspNetCore.Mvc;
using FinanceDashboard.Data;
using FinanceDashboard.Models;
using FinanceDashboard.DTOs.Transaction;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace FinanceDashboard.Controllers;

[Authorize(Policy = "AdminOrAnalyst")]
[ApiController]
[Route("api/transaction")]
public class TransactionController(ITransactionService transactionService) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet]
    async public Task<IActionResult> GetAll()
    {
        var transactions = await transactionService.GetAllAsync();
        return Ok(transactions);
    }

    [HttpGet("{id}")]
    async public Task<IActionResult> GetById(int id)
    {
        var t = await transactionService.GetByIdAsync(id);
        if (t == null) return NotFound();
        return Ok(t);
    }

    [HttpPost]
    async public Task<IActionResult> Create(CreateTransactionDto dto)
    {
        var transaction = await transactionService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = transaction.Id }, transaction);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateTransactionDto dto)
    {
        await transactionService.UpdateAsync(id, dto);
        return NoContent();
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("{id}")]
    async public Task<IActionResult> Delete(int id)
    {
        await transactionService.DeleteAsync(id);
        return NoContent();
    }
}