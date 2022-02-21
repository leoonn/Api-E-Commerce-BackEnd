using Login_Backend.Data;
using Login_Backend.Dtos;
using Login_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login_Backend.Services
{
    public class UserService
    {
        private readonly LoginDbContext _context;

        public UserService(LoginDbContext context)
        {
            _context = context;
        }

        public async Task<UserDtos> GetById(int userId)
        {
            UserDtos user = await _context.User.Select(u => new UserDtos()
            {
                Id = u.Id,
                Name = u.Name,
                LastName = u.LastName,
            }).FirstAsync(obj => obj.Id == userId);
            return user;
        }
    }
}
