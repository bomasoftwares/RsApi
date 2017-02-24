using Boma.RedeSocial.Domain.Users.Interfaces;
using System;

namespace Boma.RedeSocial.Domain.Context.Interfaces
{
    public interface ISexMoveContext
    {
        Guid Id { get; set; }
        IUser User { get; set; }

    }
}
