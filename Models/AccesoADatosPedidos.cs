using System.Text.Json;
namespace RepasoTPS;

public class AccesoADatosPedidos
{
    private string ruta;

    public AccesoADatosPedidos(string ruta)
    {
        this.ruta = ruta;
    }

    public List<Pedido> Obtener()
    {
        List<Pedido> pedidos = new List<Pedido>();
        if (AccesoADatos.ExisteArchivo(ruta))
        {
            string TextoJson = File.ReadAllText(ruta);
            pedidos = JsonSerializer.Deserialize<List<Pedido>>(TextoJson);
        }
        return pedidos;
    }
    public void Guardar(List<Pedido> pedidos)
    {
        string formatoJson = JsonSerializer.Serialize(pedidos);
        File.WriteAllText(ruta, formatoJson);
    }
}