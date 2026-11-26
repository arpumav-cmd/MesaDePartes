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
                // Búsqueda lineal por código (recursiva). Devuelve la posición o -1 si no existe.
        public static int BuscarIndice(Expediente[] exps, int cantidad, string codigo, int k = 0)
        {
            if (k >= cantidad) return -1;                    // caso base: se acabó el arreglo
            if (exps[k].Codigo == codigo) return k;          // caso base: encontrado
            return BuscarIndice(exps, cantidad, codigo, k + 1);   // caso recursivo
        }
 
        // Búsqueda binaria por código (recursiva). La lista debe estar ordenada por código.
        // Se llama con izq = 0 y der = cantidad - 1; 'comparaciones' empieza en 0 y se va acumulando.
        public static int BuscarBinario(Expediente[] exps, string codigo, int izq, int der, ref int comparaciones)
        {
            if (izq > der) return -1;                        // caso base: no existe
            int medio = (izq + der) / 2;
            comparaciones++;
            int c = string.Compare(exps[medio].Codigo, codigo, StringComparison.Ordinal);
            if (c == 0) return medio;                        // caso base: encontrado
            if (c < 0) return BuscarBinario(exps, codigo, medio + 1, der, ref comparaciones);   // caso recursivo: mitad derecha
            return BuscarBinario(exps, codigo, izq, medio - 1, ref comparaciones);              // caso recursivo: mitad izquierda
        }
 
        // Versiones con ciclo (para comparar con las anteriores en el informe).
        public static int BuscarIndiceIterativo(Expediente[] exps, int cantidad, string codigo, out int comparaciones)

        {
            int pos = -1;
            int k = 0;
            comparaciones = 0;
            while (k < cantidad && pos == -1)
            {
                comparaciones++;
                if (exps[k].Codigo == codigo)
                    pos = k;
                k++;
            }
            return pos;
        }
 
        public static int BuscarBinarioIterativo(Expediente[] exps, int cantidad, string codigo, out int comparaciones)

        {
                        int izq = 0;
            int der = cantidad - 1;
            comparaciones = 0;
            while (izq <= der)
            {
                int medio = (izq + der) / 2;
                comparaciones++;
                int c = string.Compare(exps[medio].Codigo, codigo, StringComparison.Ordinal);
                if (c == 0) return medio;
                if (c < 0) izq = medio + 1;
                else der = medio - 1;
            }
            return -1;
        }
         // ¿La lista está ordenada por código? (necesario para la búsqueda binaria) - recursiva
        public static bool EstaOrdenadoPorCodigo(Expediente[] exps, int cantidad, int k = 0)
        {
            if (k >= cantidad - 1) return true;              // caso base: no quedan pares por revisar
            if (string.Compare(exps[k].Codigo, exps[k + 1].Codigo, StringComparison.Ordinal) > 0)
                return false;                                // caso base: se encontró un par desordenado
            return EstaOrdenadoPorCodigo(exps, cantidad, k + 1);   // caso recursivo
        }
 
        // Compara dos expedientes. criterio: 1 = código, 2 = apellido (y nombres si hay empate).
        public static int Comparar(Expediente a, Expediente b, int criterio)
        {
            if (criterio == 1)
                return string.Compare(a.Codigo, b.Codigo, StringComparison.Ordinal);
            string ka = (a.Apellidos + " " + a.Nombres).ToUpper();
            string kb = (b.Apellidos + " " + b.Nombres).ToUpper();
            return string.Compare(ka, kb, StringComparison.InvariantCulture);
        }
 
        // Una pasada del método burbuja. Devuelve cuántos intercambios hizo.
        // El conteo de comparaciones e intercambios totales se acumula en los parámetros ref.
        public static int PasadaBurbuja(Expediente[] exps, int cantidad, int pasada, int criterio,
                                        ref int comparaciones, ref int intercambios)
        {
            int cambiosPasada = 0;
            for (int j = 0; j <= cantidad - pasada - 2; j++)
            {
                comparaciones++;
                if (Comparar(exps[j], exps[j + 1], criterio) > 0)
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
