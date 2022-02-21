using Login_Backend.Data;
using Login_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login_Backend.Services
{
    public class OrderServices
    {
        private readonly LoginDbContext _context;

        public OrderServices(LoginDbContext context)
        {
            _context = context;
        }
        
        public async Task<List<Order>> GetAllOrders(int userId)
        {
          return await  _context.Order.Include(obj => obj.Cart).Where(obj => obj.UserId == userId).ToListAsync();
        }
        public async Task<Order> GetOrderById(int id)
        {
            return await _context.Order.Include(obj => obj.Cart).Where(obj => obj.Id == id).FirstAsync();
        }
    }
}
