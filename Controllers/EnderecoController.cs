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
    public class EnderecoController : Controller
    {
        //private readonly LocarDbContext _contexto;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IEnderecoRepositorio _enderecoRepositorio;
        private readonly ILogger<EnderecoController> _logger;

        public EnderecoController(IUsuarioRepositorio usuarioRepositorio, IEnderecoRepositorio enderecoRepositorio, ILogger<EnderecoController> logger)
        {
            _enderecoRepositorio = enderecoRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _logger = logger;
        }


        // GET: Endereco/Create
        public async Task<IActionResult> CriarEndereco()
        {
            _logger.LogInformation("Criando um novo endereço...");
            var usuario = await _usuarioRepositorio.PegarUsuarioLogado(User);
            var endereco = new Endereco { UsuarioId = usuario.Id };


            return View(endereco);
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarEndereco([Bind("EnderecoId,Estado,Cidade,Bairro,Rua,Numero,UsuarioId")] Endereco endereco)
        {
            if (ModelState.IsValid)
            {
                await _enderecoRepositorio.Inserir(endereco);
                _logger.LogInformation("Inserido endereco com sucesso...");

                return RedirectToAction("Inicio", "Usuario");
            }

            _logger.LogError("Erro ao tentar registrar novo endereço...");

            return View(endereco);
        }

        
        public async Task<IActionResult> EditarEndereco(int id)
        {
            _logger.LogInformation("Atualizando novo endereco....");
            var endereco = await _enderecoRepositorio.PegarPeloId(id);

            if (endereco == null)
            {
                _logger.LogError("Endereço não encontrado...");

                return NotFound();
            }

            return View(endereco);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarEndereco(int id, [Bind("EnderecoId,Estado,Cidade,Bairro,Rua,Numero,UsuarioId")] Endereco endereco)
        {
            if (id != endereco.EnderecoId)
            {
                _logger.LogError("Erro tentar encontrar endereco....");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _enderecoRepositorio.Atualizar(endereco);
                _logger.LogInformation("Endereço atualizado...");
                return RedirectToAction("Inicio","Usuario");
            }
            
            _logger.LogError("Erro tentar encontrar endereco....");

            return View(endereco);
        }

     
        [HttpPost]
        public async Task<IActionResult> DeletarEndereco(int id)
        {
            await _enderecoRepositorio.Excluir(id);
            _logger.LogInformation("Endereço Excluido...");

            return Json("Endereço Excluido....");
        }

    }
}
