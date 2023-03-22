using PVP_Projektas_API.Models;

namespace PVP_Projektas_API.Interfaces
{
    public interface IShelfRepository
    {
        public Task<Shelf> CreateDefaultShelf(User user);
        public Task<List<Shelf>> GetAllShelves();
    }
}
