using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DayTraderProAPI.Core.Entities.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace DayTraderProAPI.Extensions
{
    public static class UserManagerExtension
    {

        public static async Task<AppUser> FindEmailWithUserNameAsync(
            this UserManager<AppUser> input, ClaimsPrincipal user)
        {
            var email = user?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            return await input.Users.Include(x => x.UserName).SingleOrDefaultAsync(x => x.Email == email);
        }

        public static async Task<AppUser> FindByEmailFromClaimPrincipal(
           this UserManager<AppUser> input, ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            return await input.Users.SingleOrDefaultAsync(x => x.Email == email);
        }
    }
}
