namespace Locar.Models
{
public class Conta
{
    public int ContaId { get; set; }

    public string UsuarioId { get; set; }
    public Usuario UsuarioVirtual {get; set;}

    public int Saldo { get; set; }
}
}