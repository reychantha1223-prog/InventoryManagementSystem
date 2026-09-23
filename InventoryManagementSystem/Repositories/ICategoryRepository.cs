using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repositories
{
    public interface ICategoryRepository
    {
        Task<DataTable> GetAllCategoriesDataTableAsync();
        Task<CategoryMetrics> GetCategoryMetricsAsync();
        Task SaveAsync(Category category);
        Task DeleteAsync(int categoryId);
    }

    public class CategoryMetrics
    {
        public int TotalCategories { get; set; }
        public int ActiveCategories { get; set; }
        public int InactiveCategories { get; set; }
        public int EmptyCategories { get; set; }
    }
}