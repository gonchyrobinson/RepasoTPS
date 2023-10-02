




namespace RepasoTPS;
public class Cadete{
    private int id;
    private string nombre;
    private string direccion;
    private string telefono;
    private List<Pedido> pedidos;
    private static float montoJornal = 500;

    public int Id { get => id; set => id = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Direccion { get => direccion; set => direccion = value; }
    public string Telefono { get => telefono; set => telefono = value; }
    public List<Pedido> Pedidos { get => pedidos; set => pedidos = value; }

    public Cadete(){
        this.pedidos=new List<Pedido>();
    }

    public Cadete(int id, string nombre, string direccion, string telefono, List<Pedido> pedidos)
    {
        this.id = id;
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
        this.pedidos = pedidos;
    }

    public Cadete(int id, string nombre, string direccion, string telefono)
    {
        this.id = id;
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
        this.pedidos=new List<Pedido>();
    }

    public float JornalACobrar(){
        int cantPed = this.pedidos.Count(ped => ped.Estado==Estado.entregado);
        return cantPed*montoJornal;
    }
    public string Mostrar(){
        string cad = "\n\t****************************Datos cadetes*********************************\n\tNombre: "+this.nombre+"\n\tDireccion: "+this.direccion+"\n\tTelefono: "+this.telefono+"\n\tListado de Pedidos: "+this.MostrarListaPedidos();
        return cad;
    }

    private string MostrarListaPedidos()
    {
        string cadena="";
        foreach (var ped in this.pedidos)
        {
            cadena+=ped.Mostrar();
            cadena+="\n";
        }
        return cadena;
    }

    public void AsingarPedido(Pedido pedido)
    {
        this.pedidos.Add(pedido);
    }

    public Pedido? TienePedido(int idPed)
    {
        return(this.pedidos.FirstOrDefault(ped => ped.Numero == idPed, null));
    }

    public bool EliminarPedido(Pedido ped)
    {
        return this.pedidos.Remove(ped);
    }

    public int CantidadPedidosEntregados()
    {
        return this.pedidos.Count(ped => ped.Estado==Estado.entregado);
    }
}