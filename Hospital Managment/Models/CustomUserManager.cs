using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Hospital_Managment.Models
{
    public class CustomUserManager :UserManager<ApplicationUser>
    {
        public CustomUserManager(IUserStore<ApplicationUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<ApplicationUser> passwordHasher,
            IEnumerable<IUserValidator<ApplicationUser>> userValidators,
            IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<ApplicationUser>> logger):base(store,optionsAccessor,passwordHasher,userValidators,passwordValidators,keyNormalizer,errors,services,logger)
        {

        }
        public async Task<string> GetUserImageUrlAsync(ApplicationUser user)
    {
        return  user.ImageUrl;
    }
    }
}
