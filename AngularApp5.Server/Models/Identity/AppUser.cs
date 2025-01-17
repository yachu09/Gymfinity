using Microsoft.AspNetCore.Identity;

namespace AngularApp5.Server.Models.Identity
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public Address Address { get; set; }
    }
}
