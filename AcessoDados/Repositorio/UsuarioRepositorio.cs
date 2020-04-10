using Locar.AcessoDados.Interface;
using Locar.Data;
using Locar.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Internal;
using Microsoft.CodeAnalysis.Emit;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Locar.AcessoDados.Repositorio
{
    public class UsuarioRepositorio : RepositorioGenerico<Usuario>, IUsuarioRepositorio
    {
        private SignInManager<Usuario> _gerenciaLogin;
        private UserManager<Usuario> _gerenciaUsuario;

        public UsuarioRepositorio(SignInManager<Usuario> gerenciaLogin, UserManager<Usuario> gerenciaUsuario, LocarDbContext context) : base(context)
        {
            _gerenciaLogin = gerenciaLogin;
            _gerenciaUsuario = gerenciaUsuario;
        }

        public async Task<Usuario> PegarUsuarioLogado(ClaimsPrincipal usuario)
        {
            return await _gerenciaUsuario.GetUserAsync(usuario);
        }

        public async Task<IdentityResult> SalvarUsuario(Usuario usuario, string senha)
        {
            return await _gerenciaUsuario.CreateAsync(usuario, senha);
        }

        public async Task AtribuirNivelAcesso(Usuario usuario, string nivelAcesso)
        {
            await _gerenciaUsuario.AddToRoleAsync(usuario, nivelAcesso);
        }

        public async Task EfetuarLogin(Usuario usuario, bool lembrar)
        {
            await _gerenciaLogin.SignInAsync(usuario, lembrar);
        }

        public async Task EfetuarLogout()
        {
            await _gerenciaLogin.SignOutAsync();
        }

        public async Task<Usuario> PegarUsuarioPorEmail(string email)
        {
            return await _gerenciaUsuario.FindByEmailAsync(email);        
        }

        public async Task AtualizarUsuario(Usuario usuario)
        {
            await _gerenciaUsuario.UpdateAsync(usuario);
        }
    }
}









