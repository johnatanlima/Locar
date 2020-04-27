using Locar.AcessoDados.Interface;
using Locar.Data;
using Locar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locar.AcessoDados.Repositorio
{
    public class CarroRepositorio : RepositorioGenerico<Carro>, ICarroRepositorio
    {
        private LocarDbContext _contexto;

        public CarroRepositorio(LocarDbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }


    }
}
