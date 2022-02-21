using Login_Backend.Data;
using Login_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login_Backend.Services
{
    public class AddressService
    {
        private readonly LoginDbContext _context;

        public AddressService(LoginDbContext loginDbContext)
        {
            _context = loginDbContext;
        }

        public async Task<List<Address>> GetAllAddress(int UserId)
        {
            return await _context.Address.Where(obj => obj.UserId == UserId).ToListAsync();
        }
        public async Task<Address> CreateAddress(Address address)
        {                    
            _context.Address.Add(address);
          await _context.SaveChangesAsync();
            return address;
        }

        public async Task<Address> UpdateAddress(long Id, Address Adress)
        {
            Address addressDb = await  _context.Address.FindAsync(Id);
            if (addressDb != null)
            {
                Adress.UserId = addressDb.UserId;
                _context.Address.Update(Adress);
                await _context.SaveChangesAsync();
                return Adress;
            }
            else
                throw new Exception("Endereço nao encontrado");
        }
        public async Task DeleteAddress(long id)
        {
            bool hasAny = await _context.Address.AnyAsync(obj => obj.Id == id);
            if (hasAny)
            {
                var Address = await _context.Address.FindAsync(id);
                _context.Address.Remove(Address);
                await _context.SaveChangesAsync();
            }
            else
                throw new Exception("Endereço nao encontrado");
        }

    }
}
