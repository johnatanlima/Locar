using Locar.AcessoDados.Repositorio;
using Locar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locar.AcessoDados.Interface
{
    public interface IContaRepositorio : IRepositorioGenerico<Conta>
    {
        new Task<IEnumerable<Conta>> PegarTodos();

        int PegarSaldoPeloId(string id);
    }
}
