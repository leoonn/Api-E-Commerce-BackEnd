using Login_Backend.Data;
using Login_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login_Backend.Services
{
    public class ProductService
    {
        private readonly LoginDbContext _context;

        public ProductService(LoginDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Product.ToListAsync();
        }

        public async Task CreatePoduct(Product product)
        {
           
           
            _context.Product.Add(product);
            await _context.SaveChangesAsync();
        }
        public async Task UpdatePoduct(Product product)
        {
            bool hasAny = await _context.Product.AnyAsync(obj => obj.Id == product.Id);
            if (hasAny)
            {
                _context.Product.Update(product);
                await _context.SaveChangesAsync();
            }
            else
                throw new Exception("Id do produto nao encontrado");
        }

        public async Task RemoveProduct(int id)
        {
          var product = await  _context.Product.FindAsync(id);
             _context.Product.Remove(product);
           await _context.SaveChangesAsync();
        }
    }
}
