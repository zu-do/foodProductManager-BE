using Microsoft.EntityFrameworkCore;
using PVP_Projektas_API.Data;
using PVP_Projektas_API.Interfaces;
using PVP_Projektas_API.Models;
using System;

namespace PVP_Projektas_API.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ProjectDbContext _dbContext;
        public AdminRepository(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Admin> CreateAdmin(UserDto data)
        {
            var admin = new Admin()
            {
                Name = data.Name,
                Lastname = data.Lastname,
                Email = data.Email,
                Password = data.Password
            };

            await _dbContext.DbAdmins.AddAsync(admin);
            await _dbContext.SaveChangesAsync();

            return admin;
        }

        public async Task<Admin?> GetAdmin(string email)
        {
            return await _dbContext.DbAdmins.FirstOrDefaultAsync(a => a.Email == email);
        }
    }
}
