using Login_Backend.Data;
using Login_Backend.Models;
using Login_Backend.Models.ViewModels;
using MercadoPago.Resource.Payment;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login_Backend.Services
{
    public class PaymentService
    {
        private readonly LoginDbContext _context;
        private readonly ShoppingCartService _serviceCart;
        public PaymentService(LoginDbContext context, ShoppingCartService serviceCart )
        {
            _context = context;
            _serviceCart = serviceCart;
        }

        public async Task<Order> CreateOrder(int userId, int AddressId, Payment payment)
        {
            Order order = new Order();
            order.UserId = userId;
            order.AddressId = AddressId;
            order.PaymentId = payment.Id;
            order.Status = payment.Status;
            List<ShoppingCart> Carts = await _context.ShoppingCart.Where(obj => obj.UserId == userId && obj.Active == true).ToListAsync();
            order.Cart = Carts;
            foreach (ShoppingCart item in Carts)
            {
                item.Active = false;
              Product product =  await _context.Product.FindAsync(item.ProductId);
                product.Stock--;
                _context.Product.Update(product);
                _context.ShoppingCart.Update(item);               
            }
            await _context.Order.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }
        public async Task<ShoppingCartViewModel> GetTotalPayment(int userId)
        {
            return await _serviceCart.GetAllShoppingCart(userId);
        }
    }
}
