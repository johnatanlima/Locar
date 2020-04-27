using System.ComponentModel.DataAnnotations;

namespace Locar.Models
{
    public class Conta
    {
        public int ContaId { get; set; }

        public string UsuarioId { get; set; }
      
        public Usuario UsuarioVirtual {get; set;}

        [Range(0, int.MaxValue, ErrorMessage ="Valor inválido")]
        [Required(ErrorMessage ="Campo obrigatório")]
        public int Saldo { get; set; }
    }
}