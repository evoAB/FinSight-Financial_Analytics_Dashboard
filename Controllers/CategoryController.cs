using Microsoft.AspNetCore.Mvc;
using FinanceDashboard.Data;
using FinanceDashboard.Models;
using FinanceDashboard.DTOs.Category;
using Microsoft.AspNetCore.Authorization;

namespace FinanceDashboard.Controllers;

[Authorize(Policy = "AdminOnly")]
[ApiController]
[Route("api/category")]
public class CategoryController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoryController(AppDbContext context)
    {
        _context = context;
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult GetAll()
    {
        var categories = _context.Categories
            .Select(c => new CategoryResponseDto
            {
                Id = c.Id,
                Name = c.Name,
                Type = c.Type
            }).ToList();

        return Ok(categories);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var c = _context.Categories.Find(id);
        if (c == null) return NotFound();

        return Ok(new CategoryResponseDto
        {
            Id = c.Id,
            Name = c.Name,
            Type = c.Type
        });
    }

    [HttpPost]
    public IActionResult Create(CreateCategoryDto dto)
    {
        var category = new Category
        {
            Name = dto.Name,
            Type = dto.Type
        };

        _context.Categories.Add(category);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateCategoryDto dto)
    {
        var c = _context.Categories.Find(id);
        if (c == null) return NotFound();

        c.Name = dto.Name;
        c.Type = dto.Type;

        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var c = _context.Categories.Find(id);
        if (c == null) return NotFound();

        _context.Categories.Remove(c);
        _context.SaveChanges();

        return NoContent();
    }
}
