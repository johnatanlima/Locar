using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Locar.AcessoDados.Interface;
using Locar.Models;
using Locar.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
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

        public async Task<IActionResult> Inicio()
        {
            _logger.LogInformation("Tela inicial de usuários...");
            if (User.Identity.IsAuthenticated)
            {
                return View(await _usuarioRepositorio.PegarUsuarioLogado(User));
            }

            return RedirectToAction("Entrar");
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

                    return RedirectToAction("Inicio", "Usuario");

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

        public async Task<IActionResult> Entrar()
        {
            if (User.Identity.IsAuthenticated)
                await _usuarioRepositorio.EfetuarLogout();

            _logger.LogInformation("Entrando na página de login...");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Entrar(LoginViewModel loginParam)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Recebendo usuário por login...");
               
                var usuario = await _usuarioRepositorio.PegarUsuarioPorEmail(loginParam.Email);

                PasswordHasher<Usuario> passwordHasher = new PasswordHasher<Usuario>();

                if(usuario != null)
                {
                    _logger.LogInformation("Carregando informações de usuário...");

                    if (passwordHasher.VerifyHashedPassword(usuario, usuario.PasswordHash, loginParam.Password) != PasswordVerificationResult.Failed)
                    {
                        _logger.LogInformation("Informações corretas...");
                        await _usuarioRepositorio.EfetuarLogin(usuario, false);

                        return RedirectToAction("Inicio", "Usuario");
                    }

                    _logger.LogInformation("Login inválido...");
                    ModelState.AddModelError("", "Email ou senha inválidos...");
                }

                _logger.LogInformation("Login inválido...");
                ModelState.AddModelError("", "Email ou senha inválidos...");
            }

            return View(loginParam);
        }

        public async Task<IActionResult> Sair()
        {
            await _usuarioRepositorio.EfetuarLogout();

            return RedirectToAction("Index", "Home");
        }
    }
}