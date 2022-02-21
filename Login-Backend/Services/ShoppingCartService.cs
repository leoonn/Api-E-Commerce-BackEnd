using Login_Backend.Data;
using Login_Backend.Dtos;
using Login_Backend.Models;
using Login_Backend.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login_Backend.Services
{
    public class ShoppingCartService
    {
        private readonly LoginDbContext _context;

        public ShoppingCartService(LoginDbContext loginDbContext)
        {
            _context = loginDbContext;
        }
        public async Task<ShoppingCartViewModel> GetAllShoppingCart(int userId)
        {
            List<ShoppingCart> list = await _context.ShoppingCart.Where(obj => obj.UserId == userId && obj.Active == true).ToListAsync();
            ShoppingCartViewModel shopp = new ShoppingCartViewModel();
            shopp.Product = new List<ProductViewModel>();

            shopp.User = await _context.User.Select(obj => new UserDtos() { 
            Id =  obj.Id,
            Name = obj.Name,
            LastName = obj.LastName
            }).FirstAsync(obj => obj.Id == userId);          

            foreach (ShoppingCart item in list)
            {
                ProductDto product = await _context.Product.Select(p => new ProductDto() {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                }).FirstOrDefaultAsync(obj => obj.Id == item.ProductId);
                ProductViewModel productVM = new ProductViewModel();
                productVM.Id = product.Id; productVM.Name = product.Name; productVM.Price = product.Price;
                productVM.Amount = item.Amount;
                productVM.SubTotal = productVM.Price * productVM.Amount; 
                shopp.Product.Add(productVM);
            }

            shopp.Total = shopp.Product.Sum(obj => obj.SubTotal);
            return shopp;
        }

        public async Task CreateShoppingCart(ShoppingCart shoppingCart)
        {
            _context.ShoppingCart.Add(shoppingCart);
            await _context.SaveChangesAsync();
        }
        public async Task<ShoppingCart> UpdateShoppingCart(int id, int amount )
        {
            bool hasAny = await _context.ShoppingCart.AnyAsync(obj => obj.Id == id);
            if (hasAny)
            {
                ShoppingCart cart = await _context.ShoppingCart.FirstAsync(c => c.Id == id);
                cart.Amount = amount;
                _context.ShoppingCart.Update(cart);
                await _context.SaveChangesAsync();
                return cart;
            }
            else
                throw new Exception("Id do produto nao encontrado");
        }

        public async Task RemoveShoppingCart(int id)
        {
            var shoppingCart = await _context.ShoppingCart.FindAsync(id);
            _context.ShoppingCart.Remove(shoppingCart);
            await _context.SaveChangesAsync();
        }
    }
}
