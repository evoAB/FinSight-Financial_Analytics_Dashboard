using Microsoft.AspNetCore.Mvc;
using FinanceDashboard.Data;
using FinanceDashboard.Models;
using FinanceDashboard.DTOs.Account;
using FinanceDashboard.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace FinanceDashboard.Controllers;

[Authorize(Policy = "AdminOrAnalyst")]
[ApiController]
[Route("api/account")]
public class AccountController(IAccountService accountService) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet]
    async public Task<IActionResult> GetAll()
    {
        var accounts = await accountService.GetAllAsync();
        return Ok(accounts);
    }

    [HttpGet("{id}")]
    async public Task<IActionResult> GetById(int id)
    {
        var result = await accountService.GetByIdAsync(id);
        return Ok(result);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPost]
    async public Task<IActionResult> Create(CreateAccountDto dto)
    {
        var account = await accountService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = account.Id }, account);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPut("{id}")]
    async public Task<IActionResult> Update(int id, UpdateAccountDto dto)
    {
        await accountService.UpdateAsync(id, dto);
        return NoContent();
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("{id}")]
    async public Task<IActionResult> Delete(int id)
    {
        await accountService.DeleteAsync(id);
        return NoContent();
    }
}

