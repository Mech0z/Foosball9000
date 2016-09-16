using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class User
    {
        public Guid Id { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Username { get; set; }

        public string GravatarEmail { get; set; }

        public string Password { get; set; }

        public List<string> Roles { get; set; }
    }
}
