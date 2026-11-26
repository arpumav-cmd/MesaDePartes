namespace MesaDePartes
{
    // ENTIDAD: un objeto Expediente reúne los datos que en el EP estaban
    // repartidos en 9 arreglos paralelos. Ahora se usa un solo arreglo Expediente[].
    public class Expediente
    {
        // Estados permitidos para un trámite.
        public static readonly string[] Estados = { "Recibido", "En trámite", "Atendido" };
 
        public string Codigo { get; private set; }   // el código no se modifica una vez registrado
        public string Dni { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public string Grado { get; set; }
        public string TipoTramite { get; set; }
        public string Asunto { get; set; }
        public string Fecha { get; set; }
        public string Estado { get; set; }
 
        public Expediente(string codigo, string dni, string apellidos, string nombres,
                          string grado, string tipoTramite, string asunto,
                          string fecha, string estado)
        {
            Codigo = codigo;
            Dni = dni;
            Apellidos = apellidos;
            Nombres = nombres;
            Grado = grado;
            TipoTramite = tipoTramite;
            Asunto = asunto;
            Fecha = fecha;
            Estado = estado;
        }
 
        // Línea que se escribe en el archivo de texto (campos separados por ';').
        public string ToLinea()
        {
            return Codigo + ";" + Dni + ";" + Apellidos + ";" + Nombres + ";" + Grado + ";"
                 + TipoTramite + ";" + Asunto + ";" + Fecha + ";" + Estado;
        }
 
        // Resumen en una línea (para listados).
        public override string ToString()
        {
            return Codigo + " | DNI: " + Dni + " | " + Apellidos + ", " + Nombres
                 + " | " + TipoTramite + " | " + Estado;
        }
 
        // Detalle completo (para búsquedas y confirmaciones).
        public string ToDetalle()
        {
            string nl = Environment.NewLine;
            return "------------------------------------" + nl
                 + "Código:        " + Codigo + nl
                 + "DNI:           " + Dni + nl
                 + "Apellidos:     " + Apellidos + nl
                 + "Nombres:       " + Nombres + nl
                 + "Grado:         " + Grado + nl
                 + "Trámite:       " + TipoTramite + nl
                 + "Asunto:        " + Asunto + nl
                 + "Fecha:         " + Fecha + nl
                 + "Estado:        " + Estado + nl
                 + "------------------------------------";
        }
    }
}
