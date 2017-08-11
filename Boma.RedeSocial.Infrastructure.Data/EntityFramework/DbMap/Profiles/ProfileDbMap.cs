using Boma.RedeSocial.Domain.Profiles.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Boma.RedeSocial.Infrastructure.Data.EntityFramework.DbMap.Profiles
{
    public class ProfileDbMap: EntityTypeConfiguration<Profile>
    {
        public ProfileDbMap()
        {
            HasKey(u => u.Id);
            HasRequired(u => u.User).WithMany().HasForeignKey(t => t.UserId);
            Property(u => u.UserId).IsRequired();

            Property(u => u.UserId)
                .HasColumnName("UserId")
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IDX_Profile_User", 1) { IsUnique = true }));

            ToTable("Profiles");
        }

    }

}
