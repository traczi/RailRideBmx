using Core.Domain.Entity;

namespace Infrastructure.Ports;

public interface ICategoryRepository
{
    public Task<Category> CreateCategoryAsync(Category category);
    public Task<Category> GetCategoryNameAsync(string categoryName);
}