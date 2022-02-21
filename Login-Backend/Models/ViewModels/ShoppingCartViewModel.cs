using Login_Backend.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login_Backend.Models.ViewModels
{
    public class ShoppingCartViewModel
    {
        public UserDtos User { get; set; }
        public List<ProductViewModel> Product { get; set; }
        public double Total { get; set; }
    }
}
