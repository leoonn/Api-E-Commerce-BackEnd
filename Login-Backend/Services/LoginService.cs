using Login_Backend.Data;
using Login_Backend.Models;
using Login_Backend.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login_Backend.Services
{
    public class LoginService
    {
        private readonly LoginDbContext _context;

        public LoginService(LoginDbContext loginDbContext)
        {
            _context = loginDbContext;
        }

        public async Task<string> Login(string email, string password) {

            password = new Cript().encript(password);
            bool hasAny = await _context.User.AnyAsync(obj => obj.Email == email.ToLower() && obj.Password == password);
            User user = await _context.User
                  .Where(u => u.Email == email.ToLower())
                  .FirstOrDefaultAsync();

            if (hasAny)
            {
                string sessionstring = new Cript().encript(email + user.Name + DateTime.Now.ToString());
                UserSession session = new UserSession();
                session.Session = sessionstring;
                session.User = user;
                List<UserSession> userSessions = await _context.UserSession.Where(obj => obj.User == user).ToListAsync();
                foreach (UserSession sessions in userSessions)
                {
                     _context.UserSession.Remove(sessions);
                }
                await _context.UserSession.AddAsync(session);
                await _context.SaveChangesAsync();
                return sessionstring;
            }
            else {
                throw new Exception("Email e senha nao conferem");
            }                             
        }

        public async Task<bool> CheckIsLogged(HttpRequest Request) {
            var response = new Cookies().ReadCookie("Session", Request);
            bool hasSession = await _context.UserSession.AnyAsync(obj => obj.Session == response);
            return hasSession; 
        } 
       
    }
}
