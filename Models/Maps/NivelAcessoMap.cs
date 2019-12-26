using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Locar.Models.Maps
{
    public class NivelAcessoMap : IEntityTypeConfiguration<NivelAcesso>
    {
        public void Configure(EntityTypeBuilder<NivelAcesso> builder)
        {
            builder.ToTable("NiveisAcesso"); 
            
            builder.Property(x => x.Permissao).IsRequired().HasMaxLength(45);         

        }
    }
}