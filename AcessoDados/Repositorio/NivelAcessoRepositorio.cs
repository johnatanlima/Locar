using Locar.AcessoDados.Interface;
using Locar.Data;
using Locar.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locar.AcessoDados.Repositorio
{
    public class NivelAcessoRepositorio : RepositorioGenerico<NivelAcesso>, INivelAcesso
    {
        private readonly RoleManager<NivelAcesso> _gerenciaNiveis;
        private readonly LocarDbContext _contexto;


        public NivelAcessoRepositorio(LocarDbContext contexto, RoleManager<NivelAcesso> gerenciaNiveis) : base(contexto)
        {
            _gerenciaNiveis = gerenciaNiveis;
            _contexto = contexto;
        }

        public async Task<bool> NivelAcessoExiste(string nivelAcessoParam)
        {
            return await _gerenciaNiveis.RoleExistsAsync(nivelAcessoParam);
        }
    }
}
