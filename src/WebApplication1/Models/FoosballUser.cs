using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class FoosballUser
    {
        public Guid Id { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Username { get; set; }

        public string GravatarEmail { get; set; }
    }
}
