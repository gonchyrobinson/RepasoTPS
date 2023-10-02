




namespace RepasoTPS;
public class Cadete{
    private int id;
    private string nombre;
    private string direccion;
    private string telefono;
    private static float montoJornal = 500;

    public int Id { get => id; set => id = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Direccion { get => direccion; set => direccion = value; }
    public string Telefono { get => telefono; set => telefono = value; }

    public Cadete(){
        
    }

    public Cadete(int id, string nombre, string direccion, string telefono)
    {
        this.id = id;
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
    }

    public string Mostrar(){
        string cad = "\n\t****************************Datos cadetes*********************************\n\tNombre: "+this.nombre+"\n\tDireccion: "+this.direccion+"\n\tTelefono: "+this.telefono;
        return cad;
    }

}