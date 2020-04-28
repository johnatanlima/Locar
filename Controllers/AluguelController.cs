using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Locar.AcessoDados.Interface;
using Locar.Models;
using Locar.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Locar.Controllers
{
    public class AluguelController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IContaRepositorio _contaRepositorio;
        private readonly IAluguelRepositorio _aluguelRepositorio;
        private readonly ILogger<AluguelController> _logger;

        public AluguelController
            (IUsuarioRepositorio usuarioRepositorio,
            IContaRepositorio contaRepositorio,
            IAluguelRepositorio aluguelRepositorio,
            ILogger<AluguelController> logger)
        {

            _usuarioRepositorio = usuarioRepositorio;
            _contaRepositorio = contaRepositorio;
            _aluguelRepositorio = aluguelRepositorio;
            _logger = logger;
        }

        public IActionResult AluguelCarro(int carroId, int precoDiaria)
        {
            var aluguel = new AluguelViewModel
            {
                CarroId = carroId,
                PrecoDiaria = precoDiaria
            };

            return View(aluguel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AluguelCarro(AluguelViewModel aluguelViewModel)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _usuarioRepositorio.PegarUsuarioLogado(User);
                var saldo = _contaRepositorio.PegarSaldoPeloId(usuario.Id);

                if (await _aluguelRepositorio.VerificarReserva(usuario.Id, 
                    aluguelViewModel.CarroId,
                    aluguelViewModel.Inicio,
                    aluguelViewModel.Fim))
                {
                    TempData["Aviso"] = "Você já possui essa reserva";
                    return View(aluguelViewModel);
                }
                else if(aluguelViewModel.PrecoTotal > saldo)
                {
                    TempData["Aviso"] = "Saldo Insuficiente";
                    return View(aluguelViewModel);
                }
                else
                {
                    Aluguel aluguel = new Aluguel
                    {
                        UsuarioId = usuario.Id,
                        CarroId = aluguelViewModel.CarroId,
                        Inicio = aluguelViewModel.Inicio,
                        Fim = aluguelViewModel.Fim,
                        PrecoTotal = aluguelViewModel.PrecoTotal
                    };

                    await _aluguelRepositorio.Inserir(aluguel);

                    var saldoUsuario = await _contaRepositorio.PegarPeloId(usuario.Id);
                    saldoUsuario.Saldo = saldoUsuario.Saldo - aluguelViewModel.PrecoTotal;
                    await _contaRepositorio.Atualizar(saldoUsuario);
                    
                    return RedirectToAction("Index", "Carro");
                }
            }

            return View(aluguelViewModel);
        }
    }
}






