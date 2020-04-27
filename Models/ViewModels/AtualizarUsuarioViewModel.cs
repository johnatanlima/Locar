using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace Locar.Models.ViewModels
{
    public class AtualizarUsuarioViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage ="Campo obrigatório")]
        [StringLength(45, ErrorMessage ="Máx de 45 caracteres")]
        public string Nome { get; set; }

        [StringLength(45, ErrorMessage = "Máx de 45 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Cpf { get; set; }

        [StringLength(45, ErrorMessage = "Máx de 45 caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Telefone { get; set; }

        public string NomeUsuario { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage ="Verifique o e-mail")]
        public string Email { get; set; }

    }
}
