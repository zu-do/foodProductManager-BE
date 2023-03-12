#nullable enable
using Microsoft.EntityFrameworkCore;
using PVP_Projektas_API.Data;
using PVP_Projektas_API.Interfaces;
using PVP_Projektas_API.Models;

namespace PVP_Projektas_API.Repository;

public class UserRepository : IUserRepository
{
    private readonly ProjectDbContext _dbContext;

    public UserRepository(ProjectDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> CreateUser(UserDto userDto, Shelf shelf)
    {
        var user = new User()
        {
            Name = userDto.Name,
            Lastname = userDto.Lastname,
            Email = userDto.Email,
            Password = userDto.Password
        };
        user.Shelves = new List<Shelf>();
        user.Shelves.Add(shelf);

        await _dbContext.DbUsers.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        return user;
    }

    public async Task<User> GetUser(string email)
    {
        return await _dbContext.DbUsers.FirstOrDefaultAsync(u => u.Email == email);
    }
}
