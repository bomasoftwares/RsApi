using Boma.RedeSocial.Domain.Profiles.Entities;
using Boma.RedeSocial.Domain.Profiles.Interfaces;
using System;
using System.Linq;

namespace Boma.RedeSocial.Infrastructure.Data.EntityFramework.Repositories.Profiles
{
    public class ProfileRepository : CommonRepository<Profile>, IProfileRepository
    {
        public ProfileRepository(SexMoveContext uow)
            :base(uow)
        {

        }

        public Profile GetByUserId(string userId) => Uow.UserProfile.FirstOrDefault(a => a.UserId == userId);
    }
}
