using Microsoft.AspNetCore.Identity;

namespace Locar.Models
{
    public class NivelAcesso : IdentityRole
    {
        public string Permissao { get; set; }
    }
}