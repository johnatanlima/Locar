using Locar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locar.AcessoDados.Interface
{
    public interface IAluguelRepositorio : IRepositorioGenerico<Aluguel>
    {
        Task<bool> VerificarReserva(string usuarioId, int carroId, string dataInicio, string dataFim);
    }
}
