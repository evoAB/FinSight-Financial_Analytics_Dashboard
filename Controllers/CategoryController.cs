using Microsoft.AspNetCore.Mvc;
using FinanceDashboard.Data;
using FinanceDashboard.Models;
using FinanceDashboard.DTOs.Category;
using Microsoft.AspNetCore.Authorization;

namespace FinanceDashboard.Controllers;

[Authorize(Policy = "AdminOnly")]
[ApiController]
[Route("api/category")]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet]
    async public Task<IActionResult> GetAll()
    {
        var categories = await categoryService.GetAllAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    async public Task<IActionResult> GetById(int id)
    {
        var result = await categoryService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    async public Task<IActionResult> Create(CreateCategoryDto dto)
    {
        var category = await categoryService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
    }

    [HttpPut("{id}")]
    async public Task<IActionResult> Update(int id, UpdateCategoryDto dto)
    {
        await categoryService.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    async public Task<IActionResult> Delete(int id)
    {
        await categoryService.DeleteAsync(id);
        return NoContent();
    }
}
