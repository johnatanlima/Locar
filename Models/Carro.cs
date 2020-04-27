using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Locar.Models
{
    public class Carro
    {
        public int CarroId { get; set; }

        [Required(ErrorMessage ="Campo obrigat�rio")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigat�rio")]
        public string Marca { get; set; }

        public string Foto { get; set; }
        [Required(ErrorMessage = "Campo obrigat�rio")]
        public int PrecoDiaria { get; set; }

        public ICollection<Aluguel> Alugueis {get; set;}
    }
}