using Login_Backend.Data;
using Login_Backend.Dtos;
using Login_Backend.Models;
using Login_Backend.Tools;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login_Backend.Services
{
    public class RegisterService
    {
        private readonly LoginDbContext _context;

        public RegisterService(LoginDbContext context)
        {
            _context = context;
        }

        public async Task<UserDtos> CreateNewUserAsync(User user)
        {
            bool hasAny = await _context.User.AnyAsync(obj => obj.Email == user.Email.ToLower());
            if (!hasAny)
            {
                user.Password = new Cript().encript(user.Password);
                user.Email = user.Email.ToLower();
                await _context.AddAsync(user);
                await _context.SaveChangesAsync();
                UserDtos userdto = new UserDtos();
                userdto.Id = user.Id;
                userdto.Name = user.Name;
                userdto.LastName = user.LastName;
                return userdto;
            }
            else {
                throw new Exception("Email Já cadastrado");               
            }
        } 
    }
}
