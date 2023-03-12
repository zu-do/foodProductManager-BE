using PVP_Projektas_API.Models;

namespace PVP_Projektas_API.Interfaces;

public interface IUserRepository
{
        Task<User> GetUser(string email);
        Task<User> CreateUser(UserDto user, Shelf shelf);

}    

