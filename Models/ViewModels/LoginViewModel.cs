using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Locar.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Campo obrigatório")]
        [DataType(DataType.EmailAddress, ErrorMessage ="Email inválido")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
