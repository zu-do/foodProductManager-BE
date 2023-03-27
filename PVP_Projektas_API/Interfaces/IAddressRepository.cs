using PVP_Projektas_API.Models;

namespace PVP_Projektas_API.Interfaces
{
    public interface IAddressRepository
    {
        Task<List<Address>> GetAddresses();
        Task<Address> CreateAddress(Address request);
    }
}
