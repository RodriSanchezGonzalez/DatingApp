using Microsoft.AspNetCore.Identity;

namespace DatingApp.API.models
{
    public class UserRole : IdentityUserRole<int>
    {
        public User User { get; set; }

        public Role Role { get; set; }
    }
}