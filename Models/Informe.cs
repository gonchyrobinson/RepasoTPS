namespace RepasoTPS;
public class Informe{
    private int totalEnvios;
    private float montoTotal;
    private int cantEnviosProm;
    private List<DatosCadete> datosCadetes;

    public Informe(List<DatosCadete> datosCadetes)
    {
        this.totalEnvios = datosCadetes.Sum(cad =>cad.CantEnvios);
        this.montoTotal = datosCadetes.Sum(cad => cad.MontoGanado);
        this.cantEnviosProm = this.totalEnvios/datosCadetes.Count();
        this.datosCadetes = datosCadetes;
    }
    public string Mostrar(){
        string cadena = "===========================================INFORME===========================================\n-Total Envios: "+this.totalEnvios+"\nMonto total ganado: "+this.montoTotal+"\nCantidad de envios promedios por cadete: "+this.cantEnviosProm+"\n------------------------------Datos cadetes---------------------------------\n";
        foreach (var cad in this.datosCadetes)
        {
            cadena+=cad.Mostrar();
            cadena+="\n";
        }
        return cadena;
    }
}
