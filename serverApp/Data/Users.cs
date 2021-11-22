using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace serverApp.Data
{
    public class Users
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
        [Required]
        public UserType UserType { get; set; }

    }
    public enum UserType
    {
        Admin=0,
        Customer=1
    }
}
