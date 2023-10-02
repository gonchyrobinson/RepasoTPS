namespace RepasoTPS;

public class AccessoADatosCSV : AccesoADatos
{
    public override Cadeteria ObtenerCadeteria(string ruta)
    {
        FileStream MiArchivo = new FileStream(ruta, FileMode.Open);
        StreamReader StrReader = new StreamReader(MiArchivo);
        string Linea = "";
        List<Cadeteria> cadeterias = new List<Cadeteria>();
        char caracter = ',';
        while ((Linea = StrReader.ReadLine()) != null)
        {
            string[] fila = Linea.Split(caracter);
            cadeterias.Add(new Cadeteria(fila[0], fila[1]));
        }
        if (cadeterias.Count() > 0)
        {
            Random rand = new Random();
            return cadeterias[rand.Next(0, cadeterias.Count())];
        }
        else
        {
            return new Cadeteria();
        }

    }
    public override List<Cadete> ObtenerCadetes(string ruta)
    {
        FileStream MiArchivo = new FileStream(ruta, FileMode.Open);
        StreamReader StrReader = new StreamReader(MiArchivo);
        string Linea = "";
        List<Cadete> cadetes = new List<Cadete>();
        char caracter = ',';
        int contador = 1;
        while ((Linea = StrReader.ReadLine()) != null)
        {
            string[] fila = Linea.Split(caracter);
            cadetes.Add(new Cadete(contador, fila[0], fila[1], fila[2]));
        }
        return cadetes;
    }
}