using POSY.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace POSY.Infra.Data.EntityConfig
{
    public class ConnectionConfig : EntityTypeConfiguration<Connection>
    {
        public ConnectionConfig()
        {
            ToTable("Connection");

            HasKey(k => k.ConnectionId);

            Property(x => x.UserAgent).HasMaxLength(250).IsRequired();
        }
    }
}
