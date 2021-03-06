﻿using Locar.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Locar.AcessoDados.Interface
{
    public interface IUsuarioRepositorio : IRepositorioGenerico<Usuario>
    {
        Task<Usuario> PegarUsuarioLogado(ClaimsPrincipal usuario);
        Task<IdentityResult> SalvarUsuario(Usuario usuario, string senha);
        Task AtualizarUsuario(Usuario usuario);
        Task AtribuirNivelAcesso(Usuario usuario, string nivelAcesso);
        Task EfetuarLogin(Usuario usuario, bool lembrar);
        Task EfetuarLogout();
        Task <Usuario> PegarUsuarioPorEmail(string email);
    }
}
