using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;

namespace RepasoTPS;
public class Cadeteria
{
    string nombre;
    string telefono;
    List<Cadete> cadetes;
    private static Cadeteria cadeteriaSingleton;
    public string Nombre { get => nombre; set => nombre = value; }
    public string Telefono { get => telefono; set => telefono = value; }
    public List<Cadete> Cadetes { get => cadetes; set => cadetes = value; }
    public Cadeteria()
    {
        this.cadetes = new List<Cadete>();
    }

    public Cadeteria(string nombre, string telefono, List<Cadete> cadetes)
    {
        this.nombre = nombre;
        this.telefono = telefono;
        this.cadetes = cadetes;
    }
    public Cadeteria(string nombre, string telefono)
    {
        this.nombre = nombre;
        this.telefono = telefono;
    }
    public static Cadeteria GetCadeteria()
    {
        if (cadeteriaSingleton == null)
        {
            AccesoADatos helper = new AccesoADatosJSON();
            string ruta1 = "./CargaArchivos/Cadeterias.json";
            string ruta2 = "./CargaArchivos/Cadetes.json";
            cadeteriaSingleton = Cadeteria.ObtenerCadeteria(helper, ruta1);
            cadeteriaSingleton.CargarCadetes(helper, ruta2);
        }
        return cadeteriaSingleton;
    }
    public string Mostrar()
    {
        string cadena = "";
        foreach (var cad in this.cadetes)
        {
            cadena += cad.Mostrar();
            cadena += "\n";
        }
        return cadena;
    }
    public bool AgregarPedido(int numero, string obs, string nombreC, string direccionC, string telefonoC, string datosReferenciaDireccionC, int idCad)
    {
        if (!ExisteIDPedido(numero))
        {
            Cliente cliente = new Cliente(nombreC, direccionC, telefonoC, datosReferenciaDireccionC);
            Pedido pedido = new Pedido(numero, obs, cliente, Estado.pendiente);
            return this.AsignarPedidoCadete(pedido, idCad);
        }
        else
        {
            return false;
        }
    }

    private bool ExisteIDPedido(int numero)
    {
        return this.cadetes.Where(cad => cad.TienePedido(numero) != null).Count() != 0;
    }

    public bool AsignarPedidoCadete(Pedido ped, int idC)
    {
        Cadete? cadete = this.EncuentraCadete(idC);
        if (cadete != null && !ExisteIDPedido(ped.Numero))
        {
            cadete.AsingarPedido(ped);
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool CambiarEstadoPedido(int idPed, Estado nuevoEstado)
    {
        Pedido? ped = EncuentraPedido(idPed);
        if (ped != null)
        {
            ped.CambiarEstado(nuevoEstado);
            return true;
        }
        else
        {
            return false;
        }
    }
    private Pedido? EncuentraPedido(int idPed)
    {
        Pedido? ped = null;
        foreach (var cad in this.cadetes)
        {
            if (ped == null)
            {
                ped = cad.TienePedido(idPed);
            }
        }
        return ped;
    }
    private Cadete? EncuentraCadete(int idCadete)
    {
        return this.cadetes.FirstOrDefault(cad => cad.Id == idCadete, null);
    }
    public bool ReasignarPedido(int idPed, int idCadNuevo)
    {
        Pedido? ped = EncuentraPedido(idPed);
        Cadete? cad = this.EncuetraCadPed(idPed);
        Cadete? cad2 = EncuentraCadete(idCadNuevo);
        bool eliminado;
        if (ped != null && cad != null && cad2 != null)
        {
            eliminado = cad.EliminarPedido(ped);
            cad2.AsingarPedido(ped);
            return eliminado;
        }
        else
        {
            return false;
        }
    }
    private Cadete? EncuetraCadPed(int idP)
    {
        Cadete? cadete = null;
        foreach (var cad in this.cadetes)
        {
            if (cad.TienePedido(idP) != null)
            {
                cadete = cad;
            }
        }
        return cadete;
    }
    public string Informe()
    {
        List<DatosCadete> datosCadetes = ObtenerDatosCadetes();
        Informe informe = new Informe(datosCadetes);
        return informe.Mostrar();

    }

    private List<DatosCadete> ObtenerDatosCadetes()
    {
        List<DatosCadete> datosCadetes = new List<DatosCadete>();
        foreach (var cad in this.cadetes)
        {
            DatosCadete datos = new DatosCadete(cad.CantidadPedidosEntregados(), cad.JornalACobrar(), cad.Id, cad.Nombre);
            datosCadetes.Add(datos);
        }

        return datosCadetes;
    }
    private List<Cadete> ObtenerCadetes(AccesoADatos helper, string ruta)
    {
        return helper.ObtenerCadetes(ruta);
    }
    private static Cadeteria ObtenerCadeteria(AccesoADatos helper, string ruta)
    {
        return helper.ObtenerCadeteria(ruta);
    }
    private void CargarCadetes(AccesoADatos helper, string ruta)
    {
        this.cadetes = ObtenerCadetes(helper, ruta);
    }
}