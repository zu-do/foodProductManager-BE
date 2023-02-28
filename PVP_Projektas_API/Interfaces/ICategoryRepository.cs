using PVP_Projektas_API.Models;

namespace PVP_Projektas_API.Repository
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategories();

        Task<Category> AddCategory(string name);
    }
}