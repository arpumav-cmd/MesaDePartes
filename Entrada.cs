namespace MesaDePartes
{
    // Cada función repite la pregunta hasta que el dato sea válido.
    public static class Entrada
    {
        // Lee una opción de un solo dígito; si escriben letras u otra cosa, devuelve 0.
        public static int LeerOpcion()
        {
            string txt = (Console.ReadLine() ?? "").Trim();
            int op = 0;
            if (txt.Length == 1 && txt[0] >= '0' && txt[0] <= '9')
                op = int.Parse(txt);
            return op;
        }

        // Pide el código y repite hasta que no esté vacío ni repetido (evita códigos duplicados).
        public static string PedirCodigo()
        {
            string cod;
            bool valido;
            do
            {
                Console.WriteLine("Ingrese el código del expediente (Ej: EXP-001):");
                cod = (Console.ReadLine() ?? "").Trim().ToUpper();
                valido = false;
                if (cod.Length < 3)
                    Console.WriteLine("ERROR: El código debe tener al menos 3 caracteres.");
                else if (Datos.BuscarIndice(cod) != -1)
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

        public static string PedirTexto(string mensaje, int minimo)
        {
            string txt;
            do
            {
                Console.WriteLine(mensaje);
                txt = (Console.ReadLine() ?? "").Trim();
                if (txt.Length < minimo)
                    Console.WriteLine("ERROR: Este campo debe tener al menos " + minimo + " caracteres.");
            } while (txt.Length < minimo);
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
                        Console.WriteLine("ERROR: Año no permitido. Debe estar entre 2020 y 2026.");
                        break;
                }
            } while (codErr != 0);
            return fec;
        }
    }
}
