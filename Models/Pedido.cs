
namespace RepasoTPS;
public enum Estado{
    pendiente = 1,
    entregado = 2,
    rechazado = 3

}
public class Pedido{
    private int numero;
    private string observacion;
    private Cliente cliente;
    private Estado estado;

    public int Numero { get => numero; set => numero = value; }
    public string Observacion { get => observacion; set => observacion = value; }
    internal Cliente Cliente { get => cliente; set => cliente = value; }
    public Estado Estado { get => estado; set => estado = value; }

    public Pedido(){
        this.cliente = new Cliente();
        this.estado=Estado.rechazado;
    }

    public Pedido(int numero, string observacion, Cliente cliente, Estado estado)
    {
        this.numero = numero;
        this.observacion = observacion;
        this.cliente = cliente;
        this.estado = estado;
    }
    public Pedido(int numero, string observacion, Estado estado, string nombreC, string direccionC, string telefonoC, string datosC)
    {
        Cliente cliente = new Cliente(nombreC, direccionC, telefonoC, datosC);
        this.numero = numero;
        this.observacion = observacion;
        this.cliente = cliente;
        this.estado = estado;
    }
    public string Mostrar(){
        string cadena = "\n\t\t\t---------------------------Datos Pedidos-------------------------------\n\t\tNumero: "+this.numero+"\n\t\tObservaciones: "+this.observacion+"\n\t\tEstado: "+this.estado+"\n\t\tDATOS CLIENTE: "+this.cliente.Mostrar();
        return cadena;
    }

    public void CambiarEstado(Estado nuevoEstado)
    {
        this.estado = nuevoEstado;
    }
}