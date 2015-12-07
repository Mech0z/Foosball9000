using System;

namespace MyModels
{
    public class ExistingUser
    {
        public Guid Id { get; set; }
        
        public string Email { get; set; }

        public string Username { get; set; }

        public string GravatarEmail { get; set; }
    }
}
