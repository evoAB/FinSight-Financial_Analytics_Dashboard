using FinanceDashboard.Data;
using FinanceDashboard.DTOs.Category;
using FinanceDashboard.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceDashboard.Services;

public class CategoryService(AppDbContext context) : ICategoryService
{
    public async Task<IEnumerable<CategoryResponseDto>> GetAllAsync()
    {
        return await context.Categories
                    .Select(c => new CategoryResponseDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Type = c.Type
                    }).ToListAsync();
    }
    public async Task<CategoryResponseDto> GetByIdAsync(int id)
    {
        var category = await context.Categories.FindAsync(id);
        if (category == null) return null;

        return new CategoryResponseDto
        {
            Id = category.Id,
            Name = category.Name,
            Type = category.Type
        };
    }
    public async Task<CategoryResponseDto> CreateAsync(CreateCategoryDto dto)
    {
        var category = new Category
        {
            Name = dto.Name,
            Type = dto.Type
        };

        context.Categories.Add(category);
        await context.SaveChangesAsync();

        return new CategoryResponseDto { Id = category.Id, Name = category.Name, Type = category.Type };

    }
    public async Task<CategoryResponseDto> UpdateAsync(int id, UpdateCategoryDto dto)
    {
        var category = await context.Categories.FindAsync(id);
        if (category == null) return null;

        category.Name = dto.Name;
        category.Type = dto.Type;

        context.SaveChanges();

        return new CategoryResponseDto { Id = category.Id, Name = category.Name, Type = category.Type };
    }
    public async Task DeleteAsync(int id)
    {
        var category = await context.Categories.FindAsync(id);
        if (category == null) return;

        context.Categories.Remove(category);
        context.SaveChanges();
    }
}