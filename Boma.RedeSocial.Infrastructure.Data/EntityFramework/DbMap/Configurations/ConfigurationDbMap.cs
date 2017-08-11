using Boma.RedeSocial.Domain.Configurations;
using System.Data.Entity.ModelConfiguration;

namespace Boma.RedeSocial.Infrastructure.Data.EntityFramework.DbMap.Configurations
{
    public class ConfigurationDbMap : EntityTypeConfiguration<Configuration>
    {
        public ConfigurationDbMap()
        {
            HasKey(f => f.UserId);

            Property(u => u.UserId).IsRequired();
            Property(p => p.Key);
            Property(p => p.Value);

            ToTable("Configurations");

        }
    }
}
