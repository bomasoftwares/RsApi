using Boma.RedeSocial.Domain.Profiles.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Boma.RedeSocial.Infrastructure.Data.EntityFramework.DbMap.Profiles
{
    public class ProfilePeopleConfigurationDbMap: EntityTypeConfiguration<ProfilePeopleConfiguration>
    {
        public ProfilePeopleConfigurationDbMap()
        {
            HasKey(u => u.Id);
            HasRequired(u => u.Profile).WithMany().HasForeignKey(t => t.ProfileId);
            Property(u => u.ProfileId).HasColumnName("ProfileId").IsRequired();

            ToTable("ProfilePeopleConfiguration");
        }
    }
}
