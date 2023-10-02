using System.Text.Json;
namespace RepasoTPS;

public class AccesoADatosJSON : AccesoADatos
{
    public override Cadeteria ObtenerCadeteria(string ruta)
    {
        List<Cadeteria> cadeteria = new List<Cadeteria>();
        if (AccesoADatos.ExisteArchivo(ruta))
        {
            string TextoJson = File.ReadAllText(ruta);
            cadeteria = JsonSerializer.Deserialize<List<Cadeteria>>(TextoJson);
        }
        if (cadeteria.Count() > 0)
        {
            Random rand = new Random();
            return cadeteria[rand.Next(0, cadeteria.Count())];
        }
        else
        {
            return new Cadeteria();
        }
    }
    public override List<Cadete> ObtenerCadetes(string ruta)
    {
        List<Cadete> cadetes = new List<Cadete>();
        if (AccesoADatos.ExisteArchivo(ruta))
        {
            string TextoJson = File.ReadAllText(ruta);
            cadetes = JsonSerializer.Deserialize<List<Cadete>>(TextoJson);
        }
        return cadetes;
    }
}