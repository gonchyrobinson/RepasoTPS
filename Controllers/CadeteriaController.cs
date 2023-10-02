using Microsoft.AspNetCore.Mvc;
namespace RepasoTPS.Controllers;


[ApiController]
[Route("[controller]")]
public class CadeteriaController : ControllerBase
{
    private readonly ILogger<CadeteriaController> _logger;
    public Cadeteria cadeteria;

    public CadeteriaController(ILogger<CadeteriaController> logger)
    {
        _logger = logger;
        cadeteria = Cadeteria.GetCadeteria();
    }
    [HttpPost("DarDeAltaPedido/numero={num}/observacion={obs}/nombreCliente={nombreC}/DireccionCliente={direcC}/TelefonoC={telefonoC}/DatosRefC={datosC}/idCad={idC}")]
    public ActionResult<string> DarDeAltaPedido(int num, string obs, string nombreC, string direcC, string telefonoC, string datosC, int idC)
    {
        bool realiza = cadeteria.AgregarPedido(num, obs, nombreC, direcC, telefonoC, datosC, idC);
        if (realiza)
        {
            return Ok("Pedido agregado con exito");
        }
        else
        {
            return BadRequest("Error al agregar pedido");
        }
    }
    [HttpPost]
    [Route("AsignarPedido/pedido={ped}/idCadete={idCad}")]
    public ActionResult<string> AsignarPedACadete(Pedido ped, int idCad)
    {
        bool realiza = cadeteria.AsignarPedidoCadete(ped, idCad);
        if (realiza)
        {
            return Ok("Pedido asignado con exito");
        }
        else
        {
            return BadRequest("Error al asignar pedido");
        }
    }
    [HttpPut]
    [Route("CambiarDeEstadoPedido/idPedido={idP}/nuevoEstado={nuevo}")]
    public ActionResult<string> CambiarDeEstadoPedido(int idP, Estado nuevo)
    {
        bool realiza = cadeteria.CambiarEstadoPedido(idP, nuevo);
        if (realiza)
        {
            return Ok("Se cambio el estado del pedido exitosamente");
        }
        else
        {
            return BadRequest("Error al cambiar estado del pedido");
        }
    }
    [HttpPut]
    [Route("ReasignarPedido/idPed={idP}/idCadeteNuevo={idC}")]
    public ActionResult<string> ReasignarPedidoAOtroCadete(int idP, int idC)
    {
        bool realiza = cadeteria.ReasignarPedido(idP, idC);
        if (realiza)
        {
            return (Ok("Pedido reasignado con exito al nuevo cadete"));
        }
        else
        {
            return BadRequest("Error al reasingar pedido");
        }

    }
    [HttpGet]
    [Route("MostrarInforme")]
    public ActionResult<string> GetInforme()
    {
        return (Ok(cadeteria.Informe()));
    }
}
