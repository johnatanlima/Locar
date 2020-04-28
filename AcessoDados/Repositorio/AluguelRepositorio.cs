using Locar.AcessoDados.Interface;
using Locar.Data;
using Locar.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locar.AcessoDados.Repositorio
{
    public class AluguelRepositorio : RepositorioGenerico<Aluguel>, IAluguelRepositorio
    {
        private readonly LocarDbContext _contexto;

        public AluguelRepositorio(LocarDbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> VerificarReserva(string usuarioId, int carroId, string dataInicio, string dataFim)
        {
            return await _contexto.Alugueis
                 .AnyAsync(a => a.UsuarioId == usuarioId &&
                     a.CarroId == carroId &&
                     a.Inicio == dataInicio &&
                     a.Fim == dataFim);
        }
    }
}
