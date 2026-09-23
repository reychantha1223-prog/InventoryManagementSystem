using System.Data;
using System.Threading.Tasks;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repositories
{
    public interface IOrderRepository
    {
        Task<DataTable> GetCustomersLookupAsync();
        Task<DataTable> GetProductsLookupAsync();
        Task<int> GetOrCreateCustomerByNameAsync(string customerName);
        Task<DataTable> GetAllOrdersAsync(string searchQuery = "", string statusFilter = "All");
        Task<DataTable> GetOrderByIdAsync(int orderId);
        Task<bool> CreateOrderAndReduceStockAsync(Order order, OrderDetail detail);
        Task<bool> UpdateOrderAsync(Order order, OrderDetail detail);
        Task<bool> DeleteOrderAndRestoreStockAsync(int orderId);
    }
}