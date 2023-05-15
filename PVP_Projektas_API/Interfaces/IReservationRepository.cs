using PVP_Projektas_API.Models;
namespace PVP_Projektas_API.Interfaces
{
    public interface IReservationRepository
    {
        Task<Reservation> Create(int productId, int GivesUserId, int TakesUserId);
    }
}
