using Microsoft.AspNetCore.Mvc;
using Proyecto1.Services;
using Proyecto1.Models;

[Route("api/[controller]")]
[ApiController]
public class TarjetasCreditoController : ControllerBase
{
  private readonly TarjetaCreditoService _tarjetaCreditoService;

  public TarjetasCreditoController(TarjetaCreditoService tarjetaCreditoService)
  {
    _tarjetaCreditoService = tarjetaCreditoService;
  }

  // GET api/tarjetascredito/{id}/saldo
  [HttpGet("{id}/saldo")]
  public ActionResult<decimal> ConsultarSaldo(int id)
  {
    try
    {
      var saldo = _tarjetaCreditoService.ConsultarSaldo(id);
      if (saldo == null)
      {
        return NotFound();
      }
      return Ok(saldo);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }

  }

  // POST api/tarjetascredito/{id}/pagos
  [HttpPost("{id}/pagos")]
  public IActionResult RealizarPago(int id, [FromBody] decimal monto)
  {
    try
    {
      _tarjetaCreditoService.RealizarPago(id, monto);
      return Ok();
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  // GET api/tarjetascredito/{id}/estadocuenta
  [HttpGet("{id}/estadocuenta")]
  public ActionResult<List<EstadoCuenta>> GenerarEstadoDeCuenta(int id)
  {
    try
    {
      var estadoCuenta = _tarjetaCreditoService.GenerarEstadoDeCuenta(id);
      if (estadoCuenta == null)
      {
        return NotFound();
      }
      return Ok(estadoCuenta);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  // GET api/tarjetascredito/{id}/movimientos
  [HttpGet("{id}/movimientos")]
  public ActionResult<Stack<Movimiento>> ConsultarMovimientos(int id)
  {
    try
    {
      var movimientos = _tarjetaCreditoService.ConsultarMovimientos(id);
      if (movimientos == null)
      {
        return NotFound();
      }
      return Ok(movimientos);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  // POST api/tarjetascredito/{id}/solicitudesaumento
  [HttpPost("{id}/solicitudesaumento")]
  public IActionResult SolicitarAumentoLimite(int id, [FromBody] decimal nuevoLimite)
  {
    try
    {
      _tarjetaCreditoService.ProcesarSolicitudAumento(id, nuevoLimite);
      return Ok();
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  // PUT api/tarjetascredito/{id}/pin
  [HttpPut("{id}/pin")]
  public IActionResult CambiarPIN(int id, [FromBody] string nuevoPIN)
  {
    _tarjetaCreditoService.CambiarPIN(id, nuevoPIN);
    return Ok();
  }

  // PUT api/tarjetascredito/{id}/bloqueo
  [HttpPut("{id}/bloqueo")]
  public IActionResult BloquearTarjeta(int id)
  {
    _tarjetaCreditoService.BloquearTarjeta(id);
    return Ok();
  }
}
