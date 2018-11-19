using CustomerApp.Core.Entity;
using Microsoft.AspNetCore.Identity;

namespace CustomerApp.Infrastructure.Data
{
    public class AuthUser: IdentityUser, IUser
    {
        
    }
}