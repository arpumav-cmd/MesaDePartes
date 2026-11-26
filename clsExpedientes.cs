namespace MesaDePartes
{
    // LÓGICA del sistema (sin lectura de teclado): se puede reutilizar desde
    // cualquier interfaz (consola, formulario, pruebas...).
    public static class clsExpedientes
    {
        public const int CapacidadInicial = 5;
 
        // Si el arreglo está lleno, crece un 50 % (igual que en el catálogo de libros).
        public static void AsegurarCapacidad(ref Expediente[] exps, int cantidad)
        {
            if (cantidad == exps.Length)
            {
                int incremento = (int)Math.Ceiling(exps.Length * 0.5);
                if (incremento < 1) incremento = 1;
                Array.Resize(ref exps, cantidad + incremento);
            }
        }
 
        public static void Agregar(ref Expediente[] exps, ref int cantidad, Expediente nuevo)
        {
            AsegurarCapacidad(ref exps, cantidad);
            exps[cantidad] = nuevo;
            cantidad++;
        }
 
        // Búsqueda lineal por código. Devuelve la posición o -1 si no existe.
        public static int BuscarIndice(Expediente[] exps, int cantidad, string codigo)
        {
            int pos = -1;
            int k = 0;
            while (k < cantidad && pos == -1)
            {
                if (exps[k].Codigo == codigo)
                    pos = k;
                k++;
            }
            return pos;
        }
 
        // Compara dos expedientes por apellido (y nombres si hay empate).
        public static int Comparar(Expediente a, Expediente b)
        {
            string ka = (a.Apellidos + " " + a.Nombres).ToUpper();
            string kb = (b.Apellidos + " " + b.Nombres).ToUpper();
            return string.Compare(ka, kb, StringComparison.Ordinal);
        }
 
        // Una pasada del método burbuja. Devuelve cuántos intercambios hizo.
        // El conteo de comparaciones e intercambios totales se acumula en los parámetros ref.
        public static int PasadaBurbuja(Expediente[] exps, int cantidad, int pasada,
                                        ref int comparaciones, ref int intercambios)
        {
            int cambiosPasada = 0;
            for (int j = 0; j <= cantidad - pasada - 2; j++)
            {
                comparaciones++;
                if (Comparar(exps[j], exps[j + 1]) > 0)
                {
                    Expediente aux = exps[j];
                    exps[j] = exps[j + 1];
                    exps[j + 1] = aux;
                    intercambios++;
                    cambiosPasada++;
                }
            }
            return cambiosPasada;
        }
 
        // Elimina la posición pos corriendo los elementos hacia atrás.
        public static void Eliminar(Expediente[] exps, ref int cantidad, int pos)
        {
            for (int k = pos; k <= cantidad - 2; k++)
                exps[k] = exps[k + 1];
            exps[cantidad - 1] = null;
            cantidad--;
        }
    }
}
