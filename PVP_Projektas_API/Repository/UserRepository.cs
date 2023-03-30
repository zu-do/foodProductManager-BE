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

    public async Task<User> CreateUser(UserDto userDto)
    {
        var user = new User()
        {
            Name = userDto.Name,
            Lastname = userDto.Lastname,
            Email = userDto.Email,
            Password = userDto.Password
        };

        _dbContext.DbUsers.Add(user);

        await _dbContext.SaveChangesAsync();

        return user;
    }

    public async Task<User?> GetUser(string email)
    {
        return await _dbContext.DbUsers.Include(sh => sh.Shelves).Include(add => add.Addresses).FirstOrDefaultAsync(u => u.Email == email);
    }

    public Task<List<Product>> GetUserProducts(string email)
    {
        throw new NotImplementedException();
    }

    public async Task<List<User>> GetUsers()
    {
        return await _dbContext.DbUsers
            .Include(it => it.Addresses)
            .ToListAsync();
    }
}
