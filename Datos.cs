namespace MesaDePartes
{
    // Arreglos paralelos: la posición i de cada arreglo pertenece al mismo expediente.
    public static class Datos
    {
        public const int Capacidad = 10;   // tamaño fijo (para pruebas; en el PSeInt puede ser 100)

        public static string[] codigos = new string[Capacidad];
        public static string[] dnis = new string[Capacidad];
        public static string[] apellidos = new string[Capacidad];
        public static string[] nombres = new string[Capacidad];
        public static string[] grados = new string[Capacidad];
        public static string[] tiposTramite = new string[Capacidad];
        public static string[] asuntos = new string[Capacidad];
        public static string[] fechas = new string[Capacidad];
        public static string[] estados = new string[Capacidad];

        public static int total = 0;      // cuántos expedientes hay registrados
        // Búsqueda lineal por código. Devuelve la posición o -1 si no existe.
        public static int BuscarIndice(string buscado)
        {
            int pos = -1;
            int k = 0;
            while (k < total && pos == -1)
            {
                if (codigos[k] == buscado)
                    pos = k;
                k++;
            }
            return pos;
        }

        // Intercambia las posiciones j y j+1 de un arreglo.
        public static void Intercambiar(string[] arreglo, int j)
        {
            string aux = arreglo[j];
            arreglo[j] = arreglo[j + 1];
            arreglo[j + 1] = aux;
        }
    }
}
