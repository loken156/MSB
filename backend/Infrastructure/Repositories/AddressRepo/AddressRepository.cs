﻿using Domain.Models.Address;

using Infrastructure.Database;
using Infrastructure.Repositories.AddressRepo;
using Microsoft.EntityFrameworkCore;

// This class implements the IAddressRepository interface and provides methods to interact with the AddressModel entities in the MSB_Database.
// It includes methods for adding, retrieving, updating, and deleting addresses.

namespace Infrastructure.Repositories.OrderRepo
{
    public class AddressRepository : IAddressRepository
    {
        private readonly MSB_Database _database;
        public AddressRepository(MSB_Database mSB_Database)
        {
            _database = mSB_Database;
        }

        public async Task<AddressModel> AddAddressAsync(AddressModel address)
        {
            await _database.Addresses.AddAsync(address);
            await _database.SaveChangesAsync();

            return await Task.FromResult(address);
        }

        public async Task<IEnumerable<AddressModel>> GetAllAddressesAsync()
        {
            return await _database.Addresses.ToListAsync();
        }

        public async Task<AddressModel> GetAddressByIdAsync(Guid addressId)
        {
            return await _database.Addresses.FindAsync(addressId);
        }

        public async Task<AddressModel> UpdateAddressAsync(AddressModel address)
        {
            _database.Addresses.Update(address);
            await _database.SaveChangesAsync();

            return address;
        }

        public async Task DeleteAddressAsync(Guid addressId)
        {
            var address = await _database.Addresses.FindAsync(addressId);
            if (address != null)
            {
                _database.Addresses.Remove(address);
                await _database.SaveChangesAsync();
            }
        }
    }
}