namespace Proyecto1.Services;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Proyecto1.Models;



public class TarjetaCreditoService
{
  private const String _path = "./datainicial.json";
  private List<TarjetaCredito> _tarjetasCredito;
  private Stack<SolicitudAumento> _pilaSolicitudesAumento;
  private Dictionary<int, LinkedList<Movimiento>> _movimientosDict;

  public TarjetaCreditoService()
  {
    _tarjetasCredito = new List<TarjetaCredito>();
    _pilaSolicitudesAumento = new Stack<SolicitudAumento>();
    _movimientosDict = new Dictionary<int, LinkedList<Movimiento>>();
    CargarDesdeJSON(_path);
  }

  public void CargarDesdeJSON(string path = _path)
  {
    string json = File.ReadAllText(path);
    _tarjetasCredito = JsonConvert.DeserializeObject<List<TarjetaCredito>>(json);
  }

  public decimal? ConsultarSaldo(int idTarjeta)
  {
    var tarjeta = _tarjetasCredito.FirstOrDefault(t => t.Id == idTarjeta);
    return tarjeta?.Saldo;
  }

  public void RealizarPago(int idTarjeta, decimal monto)
  {
    var tarjeta = _tarjetasCredito.FirstOrDefault(t => t.Id == idTarjeta);
    if (tarjeta == null) throw new Exception("Tarjeta no encontrada");
    if (tarjeta.Bloqueada) throw new Exception("Tarjeta bloqueada");
    if (tarjeta.Saldo < monto) throw new Exception("Saldo insuficiente");


    tarjeta.Saldo -= monto;

    // Registrar el movimiento en el historial de movimientos
    if (!_movimientosDict.ContainsKey(idTarjeta))
    {
      _movimientosDict[idTarjeta] = new LinkedList<Movimiento>();
    }
    _movimientosDict[idTarjeta].AddFirst(new Movimiento { Tipo = "Pago", Monto = monto, Fecha = DateTime.Now });
    tarjeta.Movimientos.Add(new Movimiento { Tipo = "Pago", Monto = monto, Fecha = DateTime.Now, Balance = tarjeta.Saldo });
    GuardarCambiosEnJSON();

  }

  public List<EstadoCuenta>? GenerarEstadoDeCuenta(int idTarjeta)
  {
    var tarjeta = _tarjetasCredito.FirstOrDefault(t => t.Id == idTarjeta);
    if (tarjeta == null) throw new Exception("Tarjeta no encontrada");

    // Generar el estado de cuenta a partir del historial de movimientos
    if (tarjeta.Movimientos.Count > 0)
    {
      var movimientos = tarjeta.Movimientos.Select(m => new EstadoCuenta { Fecha = m.Fecha, Descripcion = m.Tipo + ": $" + m.Monto + " Balance: $" + m.Balance }).ToList();
      movimientos.Add(new EstadoCuenta { Fecha = DateTime.Now, Descripcion = "Saldo Actual: $" + tarjeta.Saldo });
      movimientos.Sort((a, b) => a.Fecha.CompareTo(b.Fecha));
      return movimientos;
    }

    return [];
  }

  public void ProcesarSolicitudAumento(int idTarjeta, decimal nuevoLimite)
  {
    var tarjeta = _tarjetasCredito.FirstOrDefault(t => t.Id == idTarjeta);
    if (nuevoLimite < 0) throw new Exception("El nuevo limite debe ser positivo");
    if (tarjeta == null) throw new Exception("Tarjeta no encontrada");
    if (tarjeta.Bloqueada) throw new Exception("La tarjeta esta bloqueada");
    if (tarjeta.Saldo > nuevoLimite) throw new Exception("El nuevo limite debe ser mayor o igual al saldo actual");

    _pilaSolicitudesAumento.Push(new SolicitudAumento { TarjetaId = idTarjeta, NuevoLimiteCredito = nuevoLimite, FechaSolicitud = DateTime.Now });

    tarjeta.Saldo = nuevoLimite;
    tarjeta.Movimientos.Add(new Movimiento { Tipo = "Aumento de limite", Monto = nuevoLimite, Fecha = DateTime.Now, Balance = nuevoLimite });
    GuardarCambiosEnJSON();
  }

  public void CambiarPIN(int idTarjeta, string nuevoPIN)
  {
    var tarjeta = _tarjetasCredito.FirstOrDefault(t => t.Id == idTarjeta);
    if (tarjeta != null)
    {
      tarjeta.PIN = nuevoPIN;
    }
    GuardarCambiosEnJSON();
  }

  public void BloquearTarjeta(int idTarjeta)
  {
    var tarjeta = _tarjetasCredito.FirstOrDefault(t => t.Id == idTarjeta);
    if (tarjeta != null)
    {
      tarjeta.Bloqueada = true;
    }

    Task.Run(() =>
      {
        Thread.Sleep(30000);
        var tarjetaDesbloqueada = _tarjetasCredito.FirstOrDefault(t => t.Id == idTarjeta);
        if (tarjetaDesbloqueada != null)
        {
          tarjetaDesbloqueada.Bloqueada = false;
        }
        GuardarCambiosEnJSON();
      });

    GuardarCambiosEnJSON();
  }

  private void GuardarCambiosEnJSON()
  {
    string json = JsonConvert.SerializeObject(_tarjetasCredito, Formatting.Indented);
    File.WriteAllText(_path, json);
  }

  public Stack<Movimiento> ConsultarMovimientos(int idTarjeta)
  {
    var tarjeta = _tarjetasCredito.FirstOrDefault(t => t.Id == idTarjeta);
    if (tarjeta == null) throw new Exception("Tarjeta no encontrada");

    var movimientos = new Stack<Movimiento>();
    foreach (var movimiento in tarjeta.Movimientos)
    {
      movimientos.Push(movimiento);
    }
    return movimientos;
  }

}






