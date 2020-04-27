using System.ComponentModel.DataAnnotations;

namespace Locar.Models
{
    public class Conta
    {
        public int ContaId { get; set; }

        public string UsuarioId { get; set; }
      
        public Usuario UsuarioVirtual {get; set;}

        [Range(0, int.MaxValue, ErrorMessage ="Valor inv�lido")]
        [Required(ErrorMessage ="Campo obrigat�rio")]
        public int Saldo { get; set; }
    }
}