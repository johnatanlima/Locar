using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Locar.Models.ViewModels
{
    public class RegistroViewModel
    {
        [Required(ErrorMessage = "Campo obrigatorio")]
        [StringLength(100, ErrorMessage = "Máximo de 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio")]
        //[StringLength(11, ErrorMessage = "Máximo de 11 caracteres")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio")]
        //[StringLength(11, ErrorMessage = "Máximo de 11 caracteres")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio")]
        [StringLength(32, ErrorMessage = "Máximo de 32 caracteres")]
        public string NomeUsuario { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Insira um e-mail válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio")]
        [DataType(DataType.Password)]
        public string Senha{ get; set; }
    }
}
