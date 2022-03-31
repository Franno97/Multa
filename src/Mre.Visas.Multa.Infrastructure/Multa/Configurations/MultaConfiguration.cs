using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mre.Visas.Multa.Infrastructure.Multa.Configurations
{
  public class MultaConfiguration : IEntityTypeConfiguration<Domain.Entities.Multa>
  {
    public void Configure(EntityTypeBuilder<Domain.Entities.Multa> builder)
    {
      builder.ToTable("Multas");

      builder.HasKey(e => e.Id);

      builder.Property(e => e.TramiteId).IsRequired(true);
      builder.Property(e => e.Estado).IsRequired(true).HasMaxLength(50);
      builder.Property(e => e.FechaRegistro).IsRequired(true);
      builder.Property(e => e.TipoMulta).IsRequired(true).HasMaxLength(50);
      builder.Property(e => e.Observacion).IsRequired(true).HasMaxLength(250);
      builder.Property(e => e.Created).IsRequired(true);
      builder.Property(e => e.CreatorId).IsRequired(true);
      builder.Property(e => e.LastModified).IsRequired(true);
      builder.Property(e => e.LastModifierId).IsRequired(true);
    }
  }
}