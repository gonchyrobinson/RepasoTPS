namespace RepasoTPS;

public class DatosCadete
{
    private int cantEnvios;
    private float montoGanado;
    private int id;
    private string nombre;

    public int CantEnvios { get => cantEnvios; set => cantEnvios = value; }
    public float MontoGanado { get => montoGanado; set => montoGanado = value; }
    public int Id { get => id; set => id = value; }
    public string Nombre { get => nombre; set => nombre = value; }

    public DatosCadete(){

    }

    public DatosCadete(int cantEnvios, float montoGanado, int id, string nombre)
    {
        this.cantEnvios = cantEnvios;
        this.montoGanado = montoGanado;
        this.id = id;
        this.nombre = nombre;
    }

    public string Mostrar(){
        string cadena ="\n\t--------------------------Datos cadete----------------------------\n\tId: "+this.id+"\n\tNombre: "+this.nombre+"\n\tTotal de envios: "+this.cantEnvios+"\n\tMonto ganado: "+this.montoGanado;
        return cadena;
    }
}