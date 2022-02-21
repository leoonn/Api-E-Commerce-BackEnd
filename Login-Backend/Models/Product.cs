using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Login_Backend.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public double Price { get; set; }

        public Product()
        {
        }

        public Product(int id, string name, string description, int stock, double price)
        {
            Id = id;
            Name = name;
            Description = description;
            Stock = stock;
            Price = price;
        }
    }
}
