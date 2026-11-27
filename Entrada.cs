namespace MesaDePartes
{
    // Lectura por teclado: cada función repite la pregunta hasta que el dato sea válido.
    public static class Entrada
    {
        // Lee una opción numérica. Si lo escrito no es un número, devuelve -1.
        public static int LeerOpcion()
        {
            int op;
            return int.TryParse(Console.ReadLine(), out op) ? op : -1;
        }

        // Muestra un mensaje y devuelve lo escrito, sin espacios sobrantes.
        public static string Leer(string mensaje)
        {
            Console.WriteLine(mensaje);
            return (Console.ReadLine() ?? "").Trim();
        }

        public static string PedirCodigo(Expediente[] exps, int cantidad)
        {
            string cod = Leer("Ingrese el código del expediente (Ej: EXP-001):").ToUpper();
            while (cod.Length < Validaciones.MinCodigo || Validaciones.TieneSeparador(cod)
                   || clsExpedientes.BuscarIndice(exps, cantidad, cod) != -1)
                cod = Leer("ERROR: código muy corto, con ';' o ya existente. Ingrese otro:").ToUpper();
            return cod;
        }

        public static string PedirDNI()
        {
            string dni = Leer("Ingrese DNI del solicitante (8 dígitos):");
            while (!Validaciones.ValidarDNI(dni))
                dni = Leer("ERROR: el DNI debe tener 8 dígitos numéricos (y no ser 00000000). Ingrese de nuevo:");
            return dni;
        }

        public static string PedirNombre(string mensaje)
        {
            string nom = Leer(mensaje);
            while (!Validaciones.ValidarNombre(nom))
                nom = Leer("ERROR: use solo letras y espacios (mínimo 2 letras). Ingrese de nuevo:");
            return nom;
        }

        // Texto libre con largo mínimo y sin ';'.
        public static string PedirTexto(string mensaje, int minimo)
        {
            string txt = Leer(mensaje);
            while (txt.Length < minimo || Validaciones.TieneSeparador(txt))
                txt = Leer("ERROR: mínimo " + minimo + " caracteres y sin el símbolo ';'. Ingrese de nuevo:");
            return txt;
        }

        public static string PedirFecha()
        {
            string fecha = Leer("Ingrese Fecha de Recepción (DD/MM/AAAA):");
            while (!Validaciones.ValidarFecha(fecha))
                fecha = Leer("ERROR: use una fecha real DD/MM/AAAA entre " + Validaciones.AñoMinimo
                             + " y " + DateTime.Now.Year + ". Ingrese de nuevo:");
            return fecha;
        }
    }
}
