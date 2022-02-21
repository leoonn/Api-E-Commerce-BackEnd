using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Login_Backend.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        [MinLength(3)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        [Required]
        public string Phone { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        public User()
        {
        }

        public User(int id, string name, string lastName, string email, string phone, string password)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Password = password;
        }
    }
}
