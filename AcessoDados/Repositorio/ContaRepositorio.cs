using Locar.AcessoDados.Interface;
using Locar.Data;
using Locar.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Locar.AcessoDados.Repositorio
{
    public class ContaRepositorio : RepositorioGenerico<Conta>, IContaRepositorio
    {
        private readonly LocarDbContext _contexto;

        public ContaRepositorio(LocarDbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public int PegarSaldoPeloId(string id)
        {
            return _contexto.Contas.FirstOrDefault(c => c.UsuarioId == id).Saldo;
        }

        public new async Task<IEnumerable<Conta>> PegarTodos()
        {
            return await _contexto.Contas.Include(c => c.UsuarioVirtual).ToListAsync();
        }
    }
}
