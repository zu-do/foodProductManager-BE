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

        public async Task<List<Address>> GetAddresses(int UserId)
        {
            return await _dbContext.DbAddresses
                .Include(it => it.User)
                .Where(it => it.UserId == UserId)
                .ToListAsync();
       }

        public async Task<Address> CreateAddress(Address request)
        {
           await _dbContext.DbAddresses.AddAsync(request);
           await _dbContext.SaveChangesAsync();
            return request;
        }

        public async Task<List<Address>?> UpdateAddress(Address request, int id)
        {
            var address = await  _dbContext.DbAddresses.FirstAsync(add => add.Id == id);
            if (address == null)
            {
                return null;
            }
            address.Comment = request.Comment;
            address.Name = request.Name;
            address.Longitude = request.Longitude;
            address.Latitude = request.Latitude;

            await _dbContext.SaveChangesAsync();
            return await _dbContext.DbAddresses.ToListAsync();
        }

        public async Task<List<Address>?> DeleteAddress(int id)
        {
            var address = await _dbContext.DbAddresses.FirstAsync(add => add.Id == id);
            if (address == null)
            {
                return null;
            } 

            _dbContext.DbAddresses.Remove(address);
            await _dbContext.SaveChangesAsync();

            return await _dbContext.DbAddresses.ToListAsync();
        }
        public async Task<Address> GetAddressByID(int Id)
        {
            return await _dbContext.DbAddresses.FirstAsync(add => add.Id == Id);


        }
    }
}
