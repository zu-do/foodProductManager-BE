using Microsoft.EntityFrameworkCore;
using PVP_Projektas_API.Data;
using PVP_Projektas_API.Interfaces;
using PVP_Projektas_API.Models;

namespace PVP_Projektas_API.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ProjectDbContext _dbContext;
        public AddressRepository(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Address>> GetAddresses()
        {
            return await _dbContext.DbAddresses
                .Include(it => it.User)
                .ToListAsync();
;       }

        public async Task<Address> CreateAddress(Address request)
        {
           await _dbContext.DbAddresses.AddAsync(request);
           await _dbContext.SaveChangesAsync();
            return request;
        }
    }
}
