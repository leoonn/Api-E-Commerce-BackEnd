using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MercadoPago.Client.Payment;
using MercadoPago.Config;
using MercadoPago.Resource.Payment;
using Login_Backend.Services;
using Login_Backend.Models.ViewModels;
using Login_Backend.Models;
using Login_Backend.Dtos;

namespace Login_Backend.Controllers
{
    [Route("Payment")]
    public class PaymentsController : Controller
    {
        private readonly PaymentService _service;
        private readonly LoginService _serviceLogin;
        private readonly UserService  _serviceUser;
        public PaymentsController(PaymentService service, LoginService loginservice, UserService serviceUser)
        {
            _service = service;
            _serviceLogin = loginservice;
            _serviceUser = serviceUser;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Route("Pay")]
        public async Task<PaymentViewModel> Payment(int userId,int AddressId, string TokenCard)
        {
            bool Logged = await _serviceLogin.CheckIsLogged(Request);
            if (Logged)
            {
                ShoppingCartViewModel cartVM = await _service.GetTotalPayment(userId);
                MercadoPagoConfig.AccessToken = Environment.GetEnvironmentVariable("SECRET_KEY");
                var request = new PaymentCreateRequest
                {
                    TransactionAmount = (decimal)cartVM.Total,
                    Token = TokenCard,
                    Description = "Payment description",
                    Installments = 1,
                    PaymentMethodId = "visa",
                    Payer = new PaymentPayerRequest
                    {
                        Email = "test.payer@email.com",
                    }
                };

                var client = new PaymentClient();

                Payment pay = await client.CreateAsync(request);
                PaymentViewModel payVM = new PaymentViewModel();
                payVM.Id = pay.Id;
                payVM.DateApproved = pay.DateApproved;
                payVM.DateCreated = pay.DateCreated;
                payVM.Payer = pay.Payer;
                payVM.PaymentMethodId = pay.PaymentMethodId;
                payVM.PaymentTypeId = pay.PaymentTypeId;
                payVM.TransactionAmount = pay.TransactionAmount;
                payVM.Status = pay.Status;
                UserDtos user = await _serviceUser.GetById(userId);
                payVM.Payer.FirstName = user.Name;
                payVM.Payer.LastName = user.LastName;
                await _service.CreateOrder(userId, AddressId, pay);
                return payVM;
            }
            else
                throw new Exception("Token Expired Please login again ");
            
               
        }
    }
}