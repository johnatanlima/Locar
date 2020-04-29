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
    public class CarrosAlugadosViewComponent : ViewComponent
    {
        private readonly LocarDbContext _contexto;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public CarrosAlugadosViewComponent(LocarDbContext contexto, IUsuarioRepositorio usuarioRepositorio)
        {
            _contexto = contexto;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var usuarioLogado = await _usuarioRepositorio.PegarUsuarioLogado(HttpContext.User);

            return View(await _contexto.Alugueis.Include(a => a.CarroVirtual).Where(a => a.UsuarioId == usuarioLogado.Id).ToListAsync());
        }
    }
}
