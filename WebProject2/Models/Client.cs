using Microsoft.AspNetCore.Identity;

namespace WebProject2.Models
{
    public class Client : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
