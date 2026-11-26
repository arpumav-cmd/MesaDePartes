namespace MesaDePartes
{
    // Lectura por teclado. Cada función repite la pregunta hasta que el dato sea válido.
    public static class Entrada
    {
        // Lee una opción numérica (puede tener varios dígitos). Si no es un número, devuelve -1.
        public static int LeerOpcion()
        {
            string txt = (Console.ReadLine() ?? "").Trim();
            int op;
            if (!int.TryParse(txt, out op))
                op = -1;

            return op;
        }
                // Muestra un mensaje y devuelve el texto escrito (sin espacios sobrantes).
        public static string PedirBusqueda(string mensaje)
        {
            Console.WriteLine(mensaje);
            return (Console.ReadLine() ?? "").Trim();
        }
 


        // Pide un código en MAYÚSCULAS; no vacío, sin ';' y sin repetir.
        public static string PedirCodigo(Expediente[] exps, int cantidad)
        {
            string cod;
            bool valido;
            do
            {
                Console.WriteLine("Ingrese el código del expediente (Ej: EXP-001):");
                cod = (Console.ReadLine() ?? "").Trim().ToUpper();
                valido = false;
                if (cod.Length < Validaciones.MinCodigo)
                    Console.WriteLine("ERROR: El código debe tener al menos " + Validaciones.MinCodigo + " caracteres.");
                else if (Validaciones.TieneSeparador(cod))
                    Console.WriteLine("ERROR: No se permite el carácter ';' en este campo.");
                else if (clsExpedientes.BuscarIndice(exps, cantidad, cod) != -1)
                    Console.WriteLine("ERROR: El código " + cod + " ya existe. Ingrese otro.");
                else
                    valido = true;
            } while (!valido);
            return cod;
        }

        public static string PedirDNI()
        {
            string dni;
            do
            {
                Console.WriteLine("Ingrese DNI del solicitante (8 dígitos numéricos):");
                dni = (Console.ReadLine() ?? "").Trim();
                if (!Validaciones.ValidarDNI(dni))
                    Console.WriteLine("ERROR: El DNI debe tener exactamente 8 dígitos numéricos (y no ser 00000000).");
            } while (!Validaciones.ValidarDNI(dni));
            return dni;
        }

        public static string PedirNombre(string mensaje)
        {
            string nom;
            do
            {
                Console.WriteLine(mensaje);
                nom = (Console.ReadLine() ?? "").Trim();
                if (!Validaciones.ValidarNombre(nom))
                    Console.WriteLine("ERROR: Use solo letras y espacios (mínimo 2 letras). No se permiten números ni símbolos.");
            } while (!Validaciones.ValidarNombre(nom));
            return nom;
        }
        // Texto libre con longitud mínima. No admite ';' (es el separador del archivo).
        public static string PedirTexto(string mensaje, int minimo)
        {
            string txt;
            bool valido;
            do
            {
                Console.WriteLine(mensaje);
                txt = (Console.ReadLine() ?? "").Trim();
                valido = false;
                if (txt.Length < minimo)
                    Console.WriteLine("ERROR: Este campo debe tener al menos " + minimo + " caracteres.");
                else if (Validaciones.TieneSeparador(txt))
                    Console.WriteLine("ERROR: No se permite el carácter ';' en este campo.");
                else
                    valido = true;
            } while (!valido);
            return txt;
        }

        public static string PedirFecha()
        {
            string fec;
            int codErr;
            do
            {
                Console.WriteLine("Ingrese Fecha de Recepción (DD/MM/AAAA):");
                fec = (Console.ReadLine() ?? "").Trim();
                codErr = Validaciones.ValidarFecha(fec);
                switch (codErr)
                {
                    case 1:
                        Console.WriteLine("ERROR: Formato incorrecto. Use DD/MM/AAAA (Ej: 20/10/2026).");
                        break;
                    case 2:
                        Console.WriteLine("ERROR: Esa fecha no existe en el calendario (revise el día y el mes).");
                        break;
                    case 3:
                        Console.WriteLine("ERROR: Año no permitido. Debe estar entre " + Validaciones.AnioMinimo
                            + " y " + Validaciones.AnioMaximo() + ".");
                        break;
                }
            } while (codErr != 0);
            return fec;
        }
    }
}
