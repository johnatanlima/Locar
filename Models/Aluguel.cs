namespace Locar.Models
{
    public class Aluguel    
    {
    public int AluguelId { get; set; }

    public string UsuarioId { get; set; }
    public Usuario UsuarioVirtual { get; set; }

    public int CarroId { get; set; }
    public Carro CarroVirtual { get; set; }

    public string Inicio { get; set; }

    public string Fim { get; set; }
    public int PrecoTotal { get; set; }
}
}