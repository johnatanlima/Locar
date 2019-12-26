using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Locar.Models.Maps
{
    public class CarroMap : IEntityTypeConfiguration<Carro>
    {
        public void Configure(EntityTypeBuilder<Carro> builder)
        {
            builder.ToTable("Carros"); 
            builder.HasKey(x => x.CarroId);

            builder.Property(x => x.Nome).IsRequired().HasMaxLength(45);         
            builder.Property(x => x.Marca).IsRequired().HasMaxLength(45);  
            builder.Property(x => x.Foto).IsRequired();
            builder.Property(x => x.PrecoDiaria).IsRequired();

            builder.HasMany(x => x.Alugueis).WithOne(x => x.CarroVirtual).OnDelete(DeleteBehavior.Cascade);
        }
    }
}