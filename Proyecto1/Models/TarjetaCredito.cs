namespace Proyecto1.Models;

public class TarjetaCredito
{
  public int Id { get; set; }
  public decimal Saldo { get; set; }
  public string PIN { get; set; } = "";
  public bool Bloqueada { get; set; }
  public List<Movimiento> Movimientos { get; set; } = new();
}