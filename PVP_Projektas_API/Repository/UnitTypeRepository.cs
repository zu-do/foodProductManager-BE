using Microsoft.EntityFrameworkCore;
using PVP_Projektas_API.Data;
using PVP_Projektas_API.Interfaces;
using PVP_Projektas_API.Models;

namespace PVP_Projektas_API.Repository
{
    public class UnitTypeRepository: IUnitTypeRepository
    {
        readonly ProjectDbContext _dbContext;
        public UnitTypeRepository(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<UnitType>> GetAllUnitTypes()
        {
            return await _dbContext.DbUnitTypes.ToListAsync();
        }
    }
}
