﻿using Locar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locar.AcessoDados.Interface
{
    public interface INivelAcesso : IRepositorioGenerico<NivelAcesso>
    {
        Task<bool> NivelAcessoExiste(string nivelAcessoParam);
    }
}
