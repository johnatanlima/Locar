using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Locar.Models
{
    public class Usuario : IdentityUser
    {
        public string Nome { get; set; }

        public string Cpf { get; set; }

        public string Telefone { get; set; }

        public ICollection<Endereco> Enderecos {get; set;}

        public ICollection<Aluguel> Alugueis {get; set;}

        
    }
}