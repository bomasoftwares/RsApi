using Boma.RedeSocial.Domain.Profiles.Entities;
using Boma.RedeSocial.Domain.Profiles.Interfaces;

namespace Boma.RedeSocial.Infrastructure.Data.EntityFramework.Repositories.Profiles
{
    public class ProfileRepository : CommonRepository<Profile>, IProfileRepository
    {
        public ProfileRepository(SexMoveContext uow)
            :base(uow)
        {

        }
    }
}
