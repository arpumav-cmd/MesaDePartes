namespace MesaDePartes
{
    // Persistencia: el archivo de texto funciona como base de datos.
    // Una línea por expediente, con 9 campos separados por ';':
    // codigo;dni;apellidos;nombres;grado;tramite;asunto;fecha;estado
    public static class clsArchivos
    {
        public const string Ruta = "Data/expedientes.txt";

        // Escribe todos los expedientes en el archivo. Devuelve false si hubo un error.
        public static bool Guardar(Expediente[] exps, int cantidad)
        {
            try
            {
                Directory.CreateDirectory("Data");
                using (StreamWriter archivo = new StreamWriter(Ruta))
                {
                    for (int i = 0; i < cantidad; i++)
                        archivo.WriteLine(exps[i].ToLinea());
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR al guardar el archivo: " + ex.Message);
                return false;
            }
        }

        // Lee el archivo y reemplaza los datos en memoria. Devuelve false si el archivo no existe.
        // 'omitidas' cuenta las líneas inválidas; si hay alguna, se crea una copia .bak del archivo.
        public static bool Cargar(ref Expediente[] exps, ref int cantidad, out int omitidas)
        {
            omitidas = 0;
            if (!File.Exists(Ruta))
                return false;

            cantidad = 0;
            try
            {
                foreach (string linea in File.ReadLines(Ruta))
                {
                    if (linea.Trim() == "") continue;             // las líneas en blanco se ignoran
                    if (!ProcesarLinea(linea, ref exps, ref cantidad))
                        omitidas++;
                }
                if (omitidas > 0)
                    File.Copy(Ruta, Ruta + ".bak", true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR al leer el archivo: " + ex.Message);
            }
            return true;
        }

        // Convierte una línea en un Expediente y lo agrega. Devuelve false si la línea no es válida.
        // Usa las mismas validaciones que el ingreso por teclado.
        private static bool ProcesarLinea(string linea, ref Expediente[] exps, ref int cantidad)
        {
            string[] d = linea.Split(';');
            if (d.Length != 9) return false;

            for (int k = 0; k < d.Length; k++)
                d[k] = d[k].Trim();
            d[0] = d[0].ToUpper();

            bool valida = d[0].Length >= Validaciones.MinCodigo
                       && clsExpedientes.BuscarIndice(exps, cantidad, d[0]) == -1      // código no repetido
                       && Validaciones.ValidarDNI(d[1])
                       && Validaciones.ValidarNombre(d[2])
                       && Validaciones.ValidarNombre(d[3])
                       && d[4].Length >= Validaciones.MinGrado
                       && d[5].Length >= Validaciones.MinTramite
                       && d[6].Length >= Validaciones.MinAsunto
                       && Validaciones.ValidarFecha(d[7])
                       && Array.IndexOf(Expediente.Estados, d[8]) != -1;
            if (!valida) return false;

            clsExpedientes.Agregar(ref exps, ref cantidad,
                new Expediente(d[0], d[1], d[2], d[3], d[4], d[5], d[6], d[7], d[8]));
            return true;
        }
    }
}
