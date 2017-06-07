using Boma.RedeSocial.Domain.Users.Entities;
using Boma.RedeSocial.Domain.Users.Interfaces;
using System;

namespace Boma.RedeSocial.Domain.Context.Interfaces
{
    public interface ISexMoveContext
    {
        Guid Id { get; set; }
        string UserContext { get; set; }

    }
}
