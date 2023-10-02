using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;

namespace RepasoTPS;
public class Cadeteria
{
    string nombre;
    string telefono;
    List<Cadete> cadetes;
    private List<Pedido> pedidos;
    private AccesoADatosCadetes datosCadetes;
    private AccesoADatosPedidos datosPedidos;
    private static Cadeteria cadeteriaSingleton;
    public string Nombre { get => nombre; set => nombre = value; }
    public string Telefono { get => telefono; set => telefono = value; }
    public List<Cadete> Cadetes { get => cadetes; set => cadetes = value; }
    public List<Pedido> Pedidos { get => pedidos; set => pedidos = value; }

    public Cadeteria()
    {
        this.cadetes = new List<Cadete>();
        this.pedidos = new List<Pedido>();
    }

    public Cadeteria(string nombre, string telefono, List<Cadete> cadetes)
    {
        this.nombre = nombre;
        this.telefono = telefono;
        this.cadetes = cadetes;
        this.pedidos = new List<Pedido>();
    }
    public Cadeteria(string nombre, string telefono)
    {
        this.nombre = nombre;
        this.telefono = telefono;
        this.pedidos=new List<Pedido>();
        this.cadetes = new List<Cadete>();
    }
    public static Cadeteria GetCadeteria()
    {
        if (cadeteriaSingleton == null)
        {
            AccesoADatosCadeteria helper = new AccesoADatosCadeteria("./CargaArchivos/Cadeterias.json");
            cadeteriaSingleton = helper.Obtener();
            cadeteriaSingleton.datosCadetes = new AccesoADatosCadetes("./CargaArchivos/Cadetes.json");
            cadeteriaSingleton.datosPedidos = new AccesoADatosPedidos("./CargaArchivos/Pedidos.json");
            cadeteriaSingleton.CargarCad();
            cadeteriaSingleton.ObtenerPed();
        }
        return cadeteriaSingleton;
    }
    public void CargarCad(){
        this.cadetes = this.datosCadetes.Obtener();
    }
    public void ObtenerPed(){
        this.pedidos = this.datosPedidos.Obtener();
    }
    public void GuardarPedidos(){
        this.datosPedidos.Guardar(this.pedidos);
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
    public bool AgregarPedido(int numero, string obs, string nombreC, string direccionC, string telefonoC, string datosReferenciaDireccionC)
    {
        if (!ExisteIDPedido(numero))
        {
            Cliente cliente = new Cliente(nombreC, direccionC, telefonoC, datosReferenciaDireccionC);
            Pedido pedido = new Pedido(numero, obs, cliente, Estado.pendiente);
            this.pedidos.Add(pedido);
            return true;
        }
        else
        {
            return false;
        }
    }
    

    private bool ExisteIDPedido(int numero)
    {
        return this.pedidos.Count(ped => ped.Numero == numero)>0;
    }

    public bool AsignarPedidoCadete(int numPed, int idC)
    {
        Pedido? pedido = EncuentraPedidoNum(numPed);
        Cadete? cadete = this.EncuentraCadete(idC);
        if (cadete != null && pedido!=null)
        {
            return pedido.AsignarCadete(cadete);
        }
        else
        {
            return false;
        }
    }
    private Pedido? EncuentraPedidoNum(int num){
        return this.pedidos.FirstOrDefault(ped => ped.Numero==num, null);
    }
    public bool CambiarEstadoPedido(int numPed, Estado nuevoEstado)
    {
        Pedido? ped = EncuentraPedidoNum(numPed);
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

    private Cadete? EncuentraCadete(int idCadete)
    {
        return this.cadetes.FirstOrDefault(cad => cad.Id == idCadete, null);
    }
    public bool ReasignarPedido(int numPed, int idCadNuevo)
    {
        Pedido? ped = EncuentraPedidoNum(numPed);
        Cadete? cad = EncuentraCadete(idCadNuevo);
        if (ped != null && cad != null)
        {
            return ped.AsignarCadete(cad);
        }
        else
        {
            return false;
        }
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
            DatosCadete datos = new DatosCadete(CantidadPedidosEntregadosCad(cad), JornalACobrarCadete(cad), cad.Id, cad.Nombre);
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
    public int CantidadPedidosEntregadosCad(int idC){
        List<Pedido> pedidosCad = GetPedidosCadete(idC);
        return pedidosCad.Count(ped => ped.Estado==Estado.entregado);
    }
    public int CantidadPedidosEntregadosCad(Cadete cad){
        List<Pedido> pedidosCad = GetPedidosCadete(cad);
        return pedidosCad.Count(ped => ped.Estado==Estado.entregado);
    }
    public float JornalACobrarCadete(int idC){
        float montoPedido = 500;
        return CantidadPedidosEntregadosCad(idC)*montoPedido;
    }
    public float JornalACobrarCadete(Cadete cad){
        float montoPedido = 500;
        return CantidadPedidosEntregadosCad(cad)*montoPedido;
    }
    private List<Pedido> GetPedidosCadete(int idC){
        List<Pedido> pedidos = this.pedidos.Where(ped => ped.GetIdCadete() == idC).ToList();
        if(pedidos.Count()>0){
            return pedidos;
        }else
        {
            return new List<Pedido>();
        }
    }
    private List<Pedido> GetPedidosCadete(Cadete cad){
        List<Pedido> pedidos = this.pedidos.Where(ped => ped.GetCadete() == cad).ToList();
        if(pedidos.Count()>0){
            return pedidos;
        }else
        {
            return new List<Pedido>();
        }
    }
    public List<Pedido> GetPedidos(){
        return this.pedidos;
    }
    public List<Cadete> GetCadetes(){
        return this.cadetes;
    }
    public bool AgregarPedido(Pedido ped){
        if (!ExisteIDPedido(ped.Numero))
        {
            this.pedidos.Add(ped);
            return true;
        }else
        {
            return false;
        }
    }

}