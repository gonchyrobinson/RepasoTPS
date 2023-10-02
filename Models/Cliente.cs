
namespace RepasoTPS;
public class Cliente
{
    private string nombre;
    private string direccion;
    private string telefono;
    private string datosReferenciaDireccion;

    public string Nombre { get => nombre; set => nombre = value; }
    public string Direccion { get => direccion; set => direccion = value; }
    public string Telefono { get => telefono; set => telefono = value; }
    public string DatosReferenciaDireccion { get => datosReferenciaDireccion; set => datosReferenciaDireccion = value; }

    public Cliente(){

    }

    public Cliente(string nombre, string direccion, string telefono, string datosReferenciaDireccion)
    {
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
        this.datosReferenciaDireccion = datosReferenciaDireccion;
    }

    public string Mostrar()
    {
        string cadena = "\n\t\tNombre: "+this.nombre+"\n\t\tDireccion: "+this.direccion+"\n\t\tTelefono: "+this.telefono+"\n\t\tDatos de referencia direccion: "+this.datosReferenciaDireccion;
        return cadena;
    }
}