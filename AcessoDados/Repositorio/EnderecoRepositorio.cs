using Locar.AcessoDados.Interface;
using Locar.Data;
using Locar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locar.AcessoDados.Repositorio
{
    public class EnderecoRepositorio : RepositorioGenerico<Endereco>, IEnderecoRepositorio
    {
        public EnderecoRepositorio(LocarDbContext contexto) : base(contexto)
        {
        }
    }
}
