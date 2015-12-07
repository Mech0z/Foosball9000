using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApplication1.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string GravatarEmail { get; set; }
        public string Name { get; set; }
    }
}
