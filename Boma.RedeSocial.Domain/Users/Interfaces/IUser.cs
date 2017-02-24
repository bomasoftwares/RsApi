using System;

namespace Boma.RedeSocial.Domain.Users.Interfaces
{
    public interface IUser
    {
        string UserName { get; set; }
        string Email { get; set; }
        Guid Id { get; set; }
    }
}
