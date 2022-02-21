using MercadoPago.Resource.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login_Backend.Models.ViewModels
{
    public class PaymentViewModel
    {
        public long? Id { get; set; }
        public PaymentPayer Payer { get; set; }
        public decimal? TransactionAmount { get; set; }
        public string  Status { get; set; }
        public DateTime? DateCreated { get; set; }       
        public DateTime? DateApproved { get; set; }
        public string PaymentMethodId { get; set; }
        public string PaymentTypeId { get; set; }
    }
}
