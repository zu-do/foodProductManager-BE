using PVP_Projektas_API.Models;

namespace PVP_Projektas_API.Interfaces;

public interface IAdminRepository
{
    public Task<Admin> CreateAdmin(UserDto data);
    public Task<Admin?> GetAdmin(string email);
}
