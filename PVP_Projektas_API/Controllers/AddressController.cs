using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PVP_Projektas_API.Interfaces;
using PVP_Projektas_API.Models;

namespace PVP_Projektas_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository _addressRepository;
        public AddressController(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        [HttpGet("getAll/{UserId}")]
        public async Task<List<Address>> GetAddresses([FromRoute] int UserId)
        {
            return await _addressRepository.GetAddresses(UserId);
        }

        [HttpPost("create")]
        public async Task<Address> CreateAddress(Address address)
        {
            return await _addressRepository.CreateAddress(address);
        }

        [HttpPut("update")]
        public async Task<List<Address>?> UpdateAddress(Address address, int id)
        {
            return await _addressRepository.UpdateAddress(address, id);
        }

        [HttpDelete("delete/{id}")]
        public async Task<List<Address>?> DeleteAddress([FromRoute] int id)
        {
            return await _addressRepository.DeleteAddress(id);
        }
    }
}
