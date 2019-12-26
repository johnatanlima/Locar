namespace Locar.Models
{

public class Endereco
{
    public int EnderecoId { get; set; }

    public string Estado { get; set; }
    public string Cidade { get; set; }

    public string Bairro { get; set; }

    public string Rua { get; set; }

    public string Numero { get; set; }

    public string UsuarioId { get; set; }
    public Usuario UsuarioVirtual { get; set; }
}
}