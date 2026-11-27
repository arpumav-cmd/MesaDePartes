namespace MesaDePartes
{
    // Entidad: un objeto Expediente reúne los 9 datos de un trámite.
    public class Expediente
    {
        public static readonly string[] Estados = { "Recibido", "En trámite", "Atendido" };

        public string Codigo { get; private set; }   // no se modifica una vez registrado
        public string Dni { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public string Grado { get; set; }
        public string TipoTramite { get; set; }
        public string Asunto { get; set; }
        public string Fecha { get; set; }
        public string Estado { get; set; }

        public Expediente(string codigo, string dni, string apellidos, string nombres, string grado,
                          string tipoTramite, string asunto, string fecha, string estado)
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

        // Línea del archivo de texto: los 9 campos separados por ';'.
        public string ToLinea()
        {
            return string.Join(";", Codigo, Dni, Apellidos, Nombres, Grado, TipoTramite, Asunto, Fecha, Estado);
        }

        // Resumen en una línea (para listados).
        public override string ToString()
        {
            return Codigo + " | DNI: " + Dni + " | " + Apellidos + ", " + Nombres + " | " + TipoTramite + " | " + Estado;
        }

        // Detalle completo (para búsquedas y confirmaciones).
        public string ToDetalle()
        {
            string nl = Environment.NewLine;
            return "------------------------------------" + nl
                 + "Código: " + Codigo + "  |  DNI: " + Dni + "  |  Estado: " + Estado + nl
                 + "Solicitante: " + Apellidos + ", " + Nombres + " (" + Grado + ")" + nl
                 + "Trámite: " + TipoTramite + "  |  Fecha: " + Fecha + nl
                 + "Asunto: " + Asunto;
        }
    }
}
