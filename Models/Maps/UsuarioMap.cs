using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Locar.Models.Maps
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");
            
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(45);
            builder.Property(x => x.Cpf).IsRequired().HasMaxLength(11);
            builder.HasIndex(x => x.Cpf).IsUnique();
            builder.Property(u => u.Telefone).IsRequired().HasMaxLength(11);

            builder.Ignore(i => i.EmailConfirmed);
            builder.Ignore(i => i.AccessFailedCount);
            builder.Ignore(i => i.LockoutEnabled);
            builder.Ignore(i => i.LockoutEnd);
            builder.Ignore(i => i.PhoneNumber);
            builder.Ignore(i => i.PhoneNumberConfirmed);
            builder.Ignore(i => i.TwoFactorEnabled);

            builder.HasMany(u => u.Enderecos).WithOne(x => x.UsuarioVirtual);
            builder.HasMany(u => u.Alugueis).WithOne(x => x.UsuarioVirtual);
            builder.HasOne(u => u.ContaVirtual).WithOne(x => x.UsuarioVirtual);


        } 
    }
}