using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Locar.Models.Maps
{
    public class AluguelMap : IEntityTypeConfiguration<Aluguel>
    {
        public void Configure(EntityTypeBuilder<Aluguel> builder)
        {
            builder.ToTable("Alugueis"); 
            builder.HasKey(x => x.AluguelId);

            builder.Property(x => x.Inicio).IsRequired();         
            builder.Property(x => x.Fim).IsRequired(); 
            builder.Property(x => x.PrecoTotal).IsRequired();

            builder.HasOne(x => x.UsuarioVirtual).WithMany(x => x.Alugueis).HasForeignKey(x => x.UsuarioId);
            builder.HasOne(x => x.CarroVirtual).WithMany(x => x.Alugueis).HasForeignKey(x => x.CarroId);

        }
    }
}