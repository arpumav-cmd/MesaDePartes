using System.Text;
 
namespace MesaDePartes
{
    // PERSISTENCIA: el archivo de texto funciona como "base de datos".
    // Formato: una línea por expediente, 9 campos separados por ';'
    // codigo;dni;apellidos;nombres;grado;tramite;asunto;fecha;estado
    public static class clsArchivos
    {
        public const string Ruta = "Data/expedientes.txt";
        private const int CamposPorLinea = 9;
 
        // Escribe todos los expedientes. Primero escribe un archivo temporal y luego
        // lo reemplaza, así el archivo real nunca queda a medias si algo falla.
        public static bool Guardar(string ruta, Expediente[] exps, int cantidad)
        {
            try
            {
                string carpeta = Path.GetDirectoryName(Path.GetFullPath(ruta));
                Directory.CreateDirectory(carpeta);
 
                string temporal = ruta + ".tmp";
                using (StreamWriter archivo = new StreamWriter(temporal, false, new UTF8Encoding(false)))
                {
                    for (int i = 0; i < cantidad; i++)
                        archivo.WriteLine(exps[i].ToLinea());
                }
                File.Move(temporal, ruta, true);
                return true;
            }
            catch (IOException ex)
            {
                Console.WriteLine("ERROR al guardar el archivo: " + ex.Message);
                return false;
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("ERROR: sin permisos para escribir el archivo: " + ex.Message);
                return false;
            }
        }
 
        // Lee el archivo y reemplaza el contenido del arreglo.
        // Devuelve false si el archivo no existe. 'omitidas' cuenta las líneas inválidas.
        public static bool Cargar(string ruta, ref Expediente[] exps, ref int cantidad, out int omitidas)
        {
            omitidas = 0;
            if (!File.Exists(ruta))
                return false;
 
            Array.Clear(exps, 0, exps.Length);
            cantidad = 0;
 
            try
            {
                using (StreamReader archivo = new StreamReader(ruta))
                {
                    while (!archivo.EndOfStream)
                    {
                        string linea = archivo.ReadLine();
                        if (linea == null || linea.Trim().Length == 0)
                            continue;                       // líneas en blanco se ignoran
 
                        if (!ProcesarLinea(linea, ref exps, ref cantidad))
                            omitidas++;
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("ERROR al leer el archivo: " + ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("ERROR: sin permisos para leer el archivo: " + ex.Message);
            }
            return true;
        }
 
        // Convierte una línea del archivo en un Expediente y lo agrega al arreglo.
        // Devuelve false si la línea no es válida (no se agrega).
        private static bool ProcesarLinea(string linea, ref Expediente[] exps, ref int cantidad)
        {
            string[] d = linea.Split(';');
            if (d.Length != CamposPorLinea)
                return false;
 
            for (int k = 0; k < d.Length; k++)
                d[k] = d[k].Trim();
            d[0] = d[0].ToUpper();
 
            if (!RegistroValido(d, exps, cantidad))
                return false;
 
            Expediente e = new Expediente(d[0], d[1], d[2], d[3], d[4], d[5], d[6], d[7], d[8]);
            clsExpedientes.Agregar(ref exps, ref cantidad, e);
            return true;
        }
 
        // Reutiliza las mismas validaciones que el ingreso por teclado.
        private static bool RegistroValido(string[] d, Expediente[] exps, int cantidad)
        {
            if (d[0].Length < Validaciones.MinCodigo) return false;
            if (clsExpedientes.BuscarIndice(exps, cantidad, d[0]) != -1) return false;   // código repetido
            if (!Validaciones.ValidarDNI(d[1])) return false;
            if (!Validaciones.ValidarNombre(d[2])) return false;
            if (!Validaciones.ValidarNombre(d[3])) return false;
            if (d[4].Length < Validaciones.MinGrado || d[5].Length < Validaciones.MinTramite || d[6].Length < Validaciones.MinAsunto) return false;
            if (Validaciones.ValidarFecha(d[7]) != 0) return false;
            return Validaciones.EsEstadoValido(d[8]);
        }
 
        // Copia de seguridad (.bak) antes de que un guardado sobrescriba un archivo con líneas inválidas.
        public static void Respaldar(string ruta)
        {
            try
            {
                if (File.Exists(ruta))
                    File.Copy(ruta, ruta + ".bak", true);
            }
            catch (IOException ex)
            {
                Console.WriteLine("No se pudo crear la copia de seguridad: " + ex.Message);
            }
        }
    }
}
