using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PVP_Projektas_API.Interfaces;
using PVP_Projektas_API.Models;

namespace PVP_Projektas_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UnitTypeController : ControllerBase
    {
        readonly IUnitTypeRepository _unitTypeRepository;

        public UnitTypeController(IUnitTypeRepository unitTypeRepository)
        {
            _unitTypeRepository = unitTypeRepository;
        }

        [HttpGet("getAll")]
        public async Task<List<UnitType>> GetAllUnitTypes()
        {
            return await _unitTypeRepository.GetAllUnitTypes();
        }
    }
}
