﻿using PVP_Projektas_API.Models;

namespace PVP_Projektas_API.Interfaces
{
    public interface IAddressRepository
    {
        Task<List<Address>> GetAddresses(int UserId);
        Task<Address> CreateAddress(Address request);
        Task<List<Address>?> UpdateAddress(Address request, int id);
        Task<List<Address>?> DeleteAddress(int id);
        Task<Address> GetAddressByID(int Id);
    }
}
