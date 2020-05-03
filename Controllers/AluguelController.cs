using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Locar.AcessoDados.Interface;
using Locar.Models;
using Locar.Models.ViewModels;
using Locar.Servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Locar.Controllers
{
    [Authorize]
    public class AluguelController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IContaRepositorio _contaRepositorio;
        private readonly IAluguelRepositorio _aluguelRepositorio;
        private readonly ILogger<AluguelController> _logger;
        private readonly IEmail _emailInterface;

        public AluguelController
            (IUsuarioRepositorio usuarioRepositorio,
            IContaRepositorio contaRepositorio,
            IAluguelRepositorio aluguelRepositorio,
            ILogger<AluguelController> logger, 
            IEmail emailInterface)
        {

            _usuarioRepositorio = usuarioRepositorio;
            _contaRepositorio = contaRepositorio;
            _aluguelRepositorio = aluguelRepositorio;
            _logger = logger;
            _emailInterface = emailInterface;
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

                    string assunto = "Reserva concluida com sucesso";
                    string mensagem = string.Format("Veiculo em aguardo. Voce pode pegá-lo dia {0}" +
                        " e deverá devolver dia {1}. O preco será R${2},00. Divirta-se", aluguel.Inicio, aluguel.Fim, aluguel.PrecoTotal);

                    await _emailInterface.EnviarEmail(usuario.Email, assunto, mensagem);

                    await _aluguelRepositorio.Inserir(aluguel);

                    var saldoUsuario = await _contaRepositorio.PegarSaldoPeloUsuarioId(usuario.Id);
                    saldoUsuario.Saldo = saldoUsuario.Saldo - aluguelViewModel.PrecoTotal;
                    await _contaRepositorio.Atualizar(saldoUsuario);
                    
                    return RedirectToAction("Index", "Carro");
                }
            }

            return View(aluguelViewModel);
        }
    }
}






