using PVP_Projektas_API.Models;

namespace PVP_Projektas_API.Interfaces
{
    public interface IShelfRepository
    {
        public Task<Shelf> CreateDefaultShelf(User user);
        public Task<List<Shelf>> GetAllShelves();
        public Task<List<Shelf>> GetUserShelves(User user);
        Task<Shelf?> AddShelfAsync(string name, int userid);
        Task<List<Shelf>> UpdateShelfAsync(ShelfDto update);
        Task<List<Shelf>?> DeleteShelf(int id, int userid);
    }
}
