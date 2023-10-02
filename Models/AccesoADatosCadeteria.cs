using System.Text.Json;
namespace RepasoTPS;

public class AccesoADatosCadeteria
{
    private string ruta;

    public AccesoADatosCadeteria(string ruta)
    {
        this.ruta = ruta;
    }

    public string Ruta { get => ruta; set => ruta = value; }

    public Cadeteria Obtener()
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
}