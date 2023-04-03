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

        [HttpGet("getAll")]
        public async Task<List<Address>> GetAddresses()
        {
            return await _addressRepository.GetAddresses();
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

        [HttpDelete("delete")]
        public async Task<List<Address>?> DeleteAddress(int id)
        {
            return await _addressRepository.DeleteAddress(id);
        }
    }
}
