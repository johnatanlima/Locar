using System.ComponentModel.DataAnnotations;

namespace Locar.Models.ViewModels
{
    public class AluguelViewModel
    {
        public int CarroId { get; set; }

        public int PrecoDiaria { get; set; }

        [Required(ErrorMessage ="Campo obrigatório")]
        public string Inicio { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Fim { get; set; }

        public int PrecoTotal { get; set; }


    }
}
