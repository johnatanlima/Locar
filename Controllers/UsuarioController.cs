using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Locar.AcessoDados.Interface;
using Locar.Models;
using Locar.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Locar.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio, ILogger<UsuarioController> logger)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Registrar()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _usuarioRepositorio.EfetuarLogout();

                _logger.LogInformation("Solicitando pagina de registro..");

                return View();
            }
           
            return View("Registrar");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar(RegistroViewModel registro)
        {
            if (ModelState.IsValid)
            {
                var usuario = new Usuario()
                {
                    Nome = registro.Nome,
                    Telefone = registro.Telefone,
                    Cpf = registro.Cpf,
                    Email = registro.Email,
                    UserName = registro.NomeUsuario,
                    PasswordHash = registro.Senha
                };

                _logger.LogInformation("Registrando novo usuario...");

                var result = await _usuarioRepositorio.SalvarUsuario(usuario, registro.Senha);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Registro feito com sucesso...");
                    _logger.LogInformation("Definindo nivel de acesso de novo usuario..");

                    var nivelAcesso = "Cliente";
                    await _usuarioRepositorio.AtribuirNivelAcesso(usuario, nivelAcesso);
                    _logger.LogInformation("Atribuicao ok..");

                    _logger.LogInformation("Realizando login automatico pós registro...");
                    await _usuarioRepositorio.EfetuarLogin(usuario, false);
                        _logger.LogInformation("Novo usuario logado com sucesso...");

                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    _logger.LogInformation("Não possivel realizar novo registro...");

                    foreach(var erro in result.Errors)
                    {
                        ModelState.AddModelError("", erro.Description.ToString());


                    }
                }

            }
            _logger.LogInformation("Informações de usuario inválidas.");
            //Se não válido model
            return View(registro);
        }
    }
}