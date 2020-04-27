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

namespace Locar.Controllers
{
    public class ContaController : Controller
    {
        
        private readonly IContaRepositorio _contaRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ILogger<ContaController> _logger;

        public ContaController(IContaRepositorio contaRepositorio, IUsuarioRepositorio usuarioRepositorio, ILogger<ContaController> logger)
        {
            _contaRepositorio = contaRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _logger = logger;
        }

      
        public async Task<IActionResult> Index()
        {
           
            return View(await _contaRepositorio.PegarTodos());
        }

        public IActionResult Create()
        {
            _logger.LogInformation("Criando uma nova conta...");
            ViewData["UsuarioId"] = new SelectList(_usuarioRepositorio.PegarTodos(), "Id", "Email");
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContaId,UsuarioId,Saldo")] Conta conta)
        {
            if (ModelState.IsValid)
            {
                await _contaRepositorio.Inserir(conta);

                _logger.LogInformation("Inserção de conta ok...");
                return RedirectToAction(nameof(Index));
            }

            _logger.LogError("Alguma deu errada na hora da consulta...");
            ViewData["UsuarioId"] = new SelectList(_usuarioRepositorio.PegarTodos(), "Id", "Email", conta.UsuarioId);
            return View(conta);
        }

        // GET: Conta/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            _logger.LogInformation("Atualizar conta...");

            var conta = await _contaRepositorio.PegarPeloId(id);

            if (conta == null)
            {
                return NotFound();
            }

            ViewData["UsuarioId"] = new SelectList(_usuarioRepositorio.PegarTodos(), "Id", "Email", conta.UsuarioId);
            return View(conta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContaId,UsuarioId,Saldo")] Conta conta)
        {
            if (id != conta.ContaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               
                await _contaRepositorio.Atualizar(conta);
                
                return RedirectToAction(nameof(Index));
            }

            ViewData["UsuarioId"] = new SelectList(await _contaRepositorio.PegarTodos(), "Id", "Email", conta.UsuarioId);
            return View(conta);
        }

        //[HttpPost]
        //public async Task<JsonResult> Delete(int id)
        //{

        //    await _contaRepositorio.Excluir(id);

        //    return Json("Conta excluída...");
        //}

    }
}
