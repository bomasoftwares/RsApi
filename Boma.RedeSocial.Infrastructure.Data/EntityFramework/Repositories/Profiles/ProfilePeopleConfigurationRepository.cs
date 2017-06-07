using Boma.RedeSocial.Domain.Context.Interfaces;
using Boma.RedeSocial.Domain.Profiles.Entities;
using Boma.RedeSocial.Domain.Profiles.Interfaces;
using Boma.RedeSocial.Domain.Users.Interfaces;

namespace Boma.RedeSocial.Infrastructure.Data.EntityFramework.Repositories.Profiles
{
    public class ProfilePeopleConfigurationRepository: CommonRepository<ProfilePeopleConfiguration>, IProfilePeopleConfigurationRepository
    {
        public ProfilePeopleConfigurationRepository(ISexMoveUnitOfWork uow, ISexMoveContext context)
            :base(uow,context)
        {

        }
    }
}
