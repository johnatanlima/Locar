using System.ComponentModel.DataAnnotations;

namespace Locar.Models
{

    public class Endereco
    {
        public int EnderecoId { get; set; }

        [StringLength(45, ErrorMessage ="Limite de 45 caracteres.")]
        [Required(ErrorMessage ="Campo obrigatório")]
        public string Estado { get; set; }

        [StringLength(45, ErrorMessage = "Limite de 45 caracteres.")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Cidade { get; set; }

        [StringLength(45, ErrorMessage = "Limite de 45 caracteres.")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Bairro { get; set; }

        [StringLength(45, ErrorMessage = "Limite de 45 caracteres.")]
        [Required(ErrorMessage ="Campo obrigatório")]
        public string Rua { get; set; }

        [Range(0, int.MaxValue, ErrorMessage ="Tamanho de dado excedido.")]
        [Required(ErrorMessage ="Campo obrigatório")]
        public string Numero { get; set; }

        public string UsuarioId { get; set; }
    
        public Usuario UsuarioVirtual { get; set; }
    }
}