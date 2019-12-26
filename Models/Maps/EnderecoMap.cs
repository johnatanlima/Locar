using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Locar.Models.Maps
{
    public class EnderecoMap : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("Enderecos"); 
            builder.HasKey(x => x.EnderecoId);

            builder.Property(x => x.Estado).IsRequired().HasMaxLength(45); 
            builder.Property(x => x.Cidade).IsRequired().HasMaxLength(45);       
            builder.Property(x => x.Bairro).IsRequired().HasMaxLength(45);       
            builder.Property(x => x.Rua).IsRequired().HasMaxLength(45);  
            builder.Property(x => x.Numero).IsRequired();

            builder.HasOne(u => u.UsuarioVirtual).WithMany(u => u.Enderecos).HasForeignKey(x => x.UsuarioId);


        }
    }
}