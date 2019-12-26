using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Locar.Models.Maps
{
    public class ContaMap : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            builder.ToTable("Contas"); 
            builder.HasKey(x => x.ContaId);
            
            builder.Property(x => x.Saldo).IsRequired();

            builder.HasOne(x => x.UsuarioVirtual).WithOne(x => x.ContaVirtual).HasForeignKey<Conta>(x => x.UsuarioId);

        }
    }
}