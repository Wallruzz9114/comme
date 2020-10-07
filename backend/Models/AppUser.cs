using Microsoft.AspNetCore.Identity;

namespace Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public Address Address { get; set; }
    }
}