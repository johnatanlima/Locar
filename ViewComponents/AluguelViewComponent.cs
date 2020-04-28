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
    public class AluguelViewComponent : ViewComponent
    {
        private readonly LocarDbContext _contexto;

        public AluguelViewComponent(LocarDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<IViewComponentResult> InvokeAsync(int carroId)
        {
            
            return View(await _contexto.Carros.FirstOrDefaultAsync(c => c.CarroId == carroId));
        }
    }
}
