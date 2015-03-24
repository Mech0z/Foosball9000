using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class User
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Name { get; set; }

        public string GravatarUsername { get; set; }
    }
}
