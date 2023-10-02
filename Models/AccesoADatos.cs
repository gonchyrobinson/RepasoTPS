using System.Net.Http.Headers;
using System.IO;
namespace RepasoTPS;
public abstract class AccesoADatos
{
    public abstract Cadeteria ObtenerCadeteria(string ruta);
    public abstract List<Cadete> ObtenerCadetes(string ruta);
    public static bool ExisteArchivo(string rutaArchivo)
    {
        if (File.Exists(rutaArchivo))
        {
            var info = new FileInfo(rutaArchivo);

            if (info.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

}
