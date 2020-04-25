using Locar.AcessoDados.Interface;
using Locar.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locar.ViewComponents
{
    public class EnderecoViewComponent : ViewComponent
    {
        private readonly LocarDbContext _contexto;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public EnderecoViewComponent(LocarDbContext contexto, IUsuarioRepositorio usuarioRepositorio)
        {
            _contexto = contexto;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var usuario = await _usuarioRepositorio.PegarUsuarioLogado(HttpContext.User);

            return View(await _contexto.Enderecos.Where(e => e.UsuarioId == usuario.Id).ToListAsync());
        }
    }
}
