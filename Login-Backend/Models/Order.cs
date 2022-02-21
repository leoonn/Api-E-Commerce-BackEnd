using Login_Backend.Models.ViewModels;
using MercadoPago.Resource.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login_Backend.Models
{
    public class Order
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public List<ShoppingCart> Cart { get; set;}
        public long? PaymentId { get; set; }
        public string Status { get; set; }

    }
}
