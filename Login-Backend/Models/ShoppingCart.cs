using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Login_Backend.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int UserId { get; set;}
        public int ProductId { get; set; }
        public int Amount { get; set;}
        public bool Active { get; set; }

        public ShoppingCart()
        {
        }

        public ShoppingCart(int id, int userId, int productId, int amount, bool active)
        {
            Id = id;
            UserId = userId;
            ProductId = productId;
            Amount = amount;
            Active = active;
        }
    }
}
