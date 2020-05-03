using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Locar.Data;
using Locar.Models;
using Locar.AcessoDados.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace Locar.Controllers
{
    [Authorize]
    public class NivelAcessoController : Controller
    {
        private readonly INivelAcesso _nivelAcesso;
        private readonly ILogger<NivelAcessoController> _logger;

        public NivelAcessoController(INivelAcesso nivelAcesso, ILogger<NivelAcessoController> logger)
        {
            _logger = logger;
            _nivelAcesso = nivelAcesso;
            
        }

        // GET: NivelAcesso
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Listando todos os registros..");
            return View(await _nivelAcesso.PegarTodos().ToListAsync());
        }

        // GET: NivelAcesso/Create
        public IActionResult Create()
        {
            _logger.LogInformation("Iniciando criação de acessos..");
            
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Permissao,Name")] NivelAcesso nivelAcesso)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Verificando se o nivel de acesso já existe...");
                bool nivelExiste = await _nivelAcesso.NivelAcessoExiste(nivelAcesso.Name);

                if (!nivelExiste)
                {
                    nivelAcesso.NormalizedName = nivelAcesso.Name.ToUpper();
                    await _nivelAcesso.Inserir(nivelAcesso);

                    _logger.LogInformation("Novo nivel de acesso criado..");

                    return RedirectToAction("Index", "NivelAcesso");
                }
            }

            _logger.LogInformation("Informaçõe passadas com erro...");
            return View(nivelAcesso);
        }

        public async Task<IActionResult> Edit(string id)
        {
            _logger.LogInformation("Atualizando informação de nivel de acesso...");
            
            if (id == null)
            {
                _logger.LogInformation("Não foi possicel encontrar o nível de acesso informado...");

                return NotFound();
            }

            var nivelAcesso = await _nivelAcesso.PegarPeloId(id);

            if (nivelAcesso == null)
            {
                _logger.LogError("Não foi possicel encontrar o nível de acesso no banco de dados...");

                return NotFound();
            }

            return View(nivelAcesso);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Permissao,Id,Name,NormalizedName,ConcurrencyStamp")] NivelAcesso nivelAcesso)
        {
            _logger.LogInformation("Atualizando nivel de acesso informado..."); 

            if (id != nivelAcesso.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _nivelAcesso.Atualizar(nivelAcesso);

                _logger.LogInformation("Atualizando nivel...");

                return RedirectToAction("Index", "NivelAcesso");
            }

            _logger.LogError("Informações inválidas..");
            return View(nivelAcesso);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _nivelAcesso.Excluir(id);
            _logger.LogInformation("Nivel de acesso excluido...");

            return RedirectToAction(nameof(Index));
        }
    }
}
