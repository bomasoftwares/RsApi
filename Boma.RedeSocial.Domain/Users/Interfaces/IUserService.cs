using Boma.RedeSocial.Domain.Users.Entities;

namespace Boma.RedeSocial.Domain.Users.Interfaces
{
    public interface IUserService
    {
        string GeneratePasswordKey(AspNetUser user);
    }
}
