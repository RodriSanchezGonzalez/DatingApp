using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DatingApp.API.models
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}