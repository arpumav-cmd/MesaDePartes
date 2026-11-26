namespace MesaDePartes
{
    public static class Validaciones
    {
         // Longitudes mínimas: se usan al escribir por teclado y al leer el archivo,
        // así si se cambia un valor, cambia en los dos sitios.
        public const int MinCodigo = 3;
        public const int MinGrado = 2;
        public const int MinTramite = 3;
        public const int MinAsunto = 5;
 
        // DNI: 8 dígitos numéricos y distinto de 00000000.
        public static bool ValidarDNI(string dni)
        {
            if (dni.Length != 8) return false;
            bool todoCeros = true;
            for (int k = 0; k < 8; k++)
            {
                if (dni[k] < '0' || dni[k] > '9') return false;
                if (dni[k] != '0') todoCeros = false;
            }
            return !todoCeros;
        }

        // Nombres y apellidos: solo letras y espacios, mínimo 2 letras.
        public static bool ValidarNombre(string nom)
        {
            string alfabeto = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZÁÉÍÓÚÜ ";
            string nomMay = nom.ToUpper();
            int letras = 0;
            for (int k = 0; k < nomMay.Length; k++)
            {
                char ch = nomMay[k];
                if (alfabeto.IndexOf(ch) == -1) return false;
                if (ch != ' ') letras++;
            }
            return letras >= 2;
        }
        // El ';' es el separador del archivo: no puede aparecer dentro de un dato.
        public static bool TieneSeparador(string texto)
        {
            return texto.IndexOf(';') != -1;
        }
 
        public static bool EsEstadoValido(string estado)
        {
            for (int k = 0; k < Expediente.Estados.Length; k++)
            {
                if (Expediente.Estados[k] == estado) return true;
            }
            return false;
        }
 
        // Días de un mes (considera años bisiestos).
        public static int DiasDelMes(int mes, int anio)
        {
            if (mes == 2)
            {
                bool bisiesto = (anio % 4 == 0 && anio % 100 != 0) || anio % 400 == 0;
                return bisiesto ? 29 : 28;
            }
            if (mes == 4 || mes == 6 || mes == 9 || mes == 11) return 30;
            return 31;
        }

        // Devuelve: 0 = válida | 1 = formato incorrecto | 2 = día o mes inexistente | 3 = año fuera de rango
        public static int ValidarFecha(string fec)
        {
        // Año máximo permitido: el año actual del sistema (antes estaba fijo en 2026).
        public const int AnioMinimo = 2020;
        public static int AnioMaximo()
        {
            return DateTime.Now.Year;
        }

            if (fec.Length != 10) return 1;
            for (int k = 0; k < 10; k++)
            {
                if (k == 2 || k == 5)
                {
                    if (fec[k] != '/') return 1;
                }
                else
                {
                    if (fec[k] < '0' || fec[k] > '9') return 1;
                }
            }

            int dia = int.Parse(fec.Substring(0, 2));
            int mes = int.Parse(fec.Substring(3, 2));
            int anio = int.Parse(fec.Substring(6, 4));

            if (anio < AnioMinimo || anio > AnioMaximo()) return 3;
            if (mes < 1 || mes > 12) return 2;
            if (dia < 1 || dia > DiasDelMes(mes, anio)) return 2;
            return 0;
        }
    }
}
