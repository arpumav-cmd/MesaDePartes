using System.Globalization;

namespace MesaDePartes
{
    public static class Validaciones
    {
        // Largos mínimos: se usan al escribir por teclado y al leer el archivo.
        public const int MinCodigo = 3;
        public const int MinGrado = 2;
        public const int MinTramite = 3;
        public const int MinAsunto = 5;
        public const int AñoMinimo = 2020;

        // Comprueba que todos los caracteres desde la posición k sean dígitos (recursiva).
        public static bool SoloDigitos(string texto, int k = 0)
        {
            if (k == texto.Length) return true;                   // caso base: se revisó todo el texto
            if (texto[k] < '0' || texto[k] > '9') return false;   // caso base: carácter que no es dígito
            return SoloDigitos(texto, k + 1);                     // caso recursivo
        }

        // DNI: 8 dígitos y distinto de 00000000.
        public static bool ValidarDNI(string dni)
        {
            return dni.Length == 8 && SoloDigitos(dni) && dni != "00000000";
        }

        // Nombres y apellidos: solo letras y espacios, con al menos 2 letras.
        public static bool ValidarNombre(string nom)
        {
            int letras = 0;
            foreach (char ch in nom)
            {
                if (char.IsLetter(ch)) letras++;
                else if (ch != ' ') return false;
            }
            return letras >= 2;
        }

        // El ';' separa los campos del archivo: no puede aparecer dentro de un dato.
        public static bool TieneSeparador(string texto)
        {
            return texto.Contains(';');
        }

        // Fecha real en formato DD/MM/AAAA, con año entre AnioMinimo y el año actual.
        public static bool ValidarFecha(string fecha)
        {
            DateTime f;
            bool formatoOk = DateTime.TryParseExact(fecha, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                                                    DateTimeStyles.None, out f);
            return formatoOk && f.Year >= AñoMinimo && f.Year <= DateTime.Now.Year;
        }
    }
}
