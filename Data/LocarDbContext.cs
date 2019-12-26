using Locar.Models;
using Locar.Models.Maps;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Locar.Data
{
    public class LocarDbContext : IdentityDbContext<Usuario, NivelAcesso, string>
    {
        public DbSet<Usuario> Usuarios {get; set;}
        public DbSet<NivelAcesso> NiveisAcesso {get; set;}
        public DbSet<Conta> Contas {get; set;}
        public DbSet<Aluguel> Alugueis {get;set;}
        public DbSet<Carro> Carros {get;set;}
        public DbSet<Endereco> Enderecos {get;set;}


        public LocarDbContext(DbContextOptions<LocarDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UsuarioMap());
            builder.ApplyConfiguration(new AluguelMap());
            builder.ApplyConfiguration(new CarroMap());
            builder.ApplyConfiguration(new EnderecoMap());
            builder.ApplyConfiguration(new NivelAcessoMap());
            builder.ApplyConfiguration(new ContaMap());
            
        }
    }
}
