
using System.ComponentModel.DataAnnotations;

namespace Login_Backend.Models
{
    public class UserSession
    {
        public int Id { get; set; }
        public string Session { get; set; }
        public User User { get; set; }

        public UserSession()
        {
        }

        public UserSession(int id, string session, User user)
        {
            Id = id;
            Session = session;
            User = user;
        }
    }
}
