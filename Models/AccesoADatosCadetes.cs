using System.Text.Json;
namespace RepasoTPS;

public class AccesoADatosCadetes{
    private string ruta;

    public AccesoADatosCadetes(string ruta)
    {
        this.ruta = ruta;
    }

    public List<Cadete> Obtener(){
        List<Cadete> cadetes = new List<Cadete>();
        if (AccesoADatos.ExisteArchivo(ruta))
        {
            string TextoJson = File.ReadAllText(ruta);
            cadetes = JsonSerializer.Deserialize<List<Cadete>>(TextoJson);
        }
        return cadetes;
    }
}