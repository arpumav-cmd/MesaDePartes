namespace MesaDePartes
{
    // Lógica del sistema: no lee el teclado ni imprime, así que se puede reutilizar.
    public static class clsExpedientes
    {
        public const int CapacidadInicial = 5;

        // Agrega un expediente. Si el arreglo está lleno, crece aproximadamente un 50 %.
        public static void Agregar(ref Expediente[] exps, ref int cantidad, Expediente nuevo)
        {
            if (cantidad == exps.Length)
                Array.Resize(ref exps, cantidad + cantidad / 2 + 1);
            exps[cantidad] = nuevo;
            cantidad++;
        }

        // Búsqueda lineal por código (recursiva). Devuelve la posición o -1 si no existe.
        public static int BuscarIndice(Expediente[] exps, int cantidad, string codigo, int k = 0)
        {
            if (k >= cantidad) return -1;                         // caso base: se acabó el arreglo
            if (exps[k].Codigo == codigo) return k;               // caso base: encontrado
            return BuscarIndice(exps, cantidad, codigo, k + 1);   // caso recursivo
        }

        // Búsqueda binaria por código (recursiva): la lista debe estar ordenada por código.
        // Se llama con izq = 0 y der = cantidad - 1; 'pasos' cuenta las comparaciones.
        public static int BuscarBinario(Expediente[] exps, string codigo, int izq, int der, ref int pasos)
        {
            if (izq > der) return -1;                             // caso base: no existe
            int medio = (izq + der) / 2;
            pasos++;
            int c = string.Compare(exps[medio].Codigo, codigo, StringComparison.Ordinal);
            if (c == 0) return medio;                             // caso base: encontrado
            if (c < 0) return BuscarBinario(exps, codigo, medio + 1, der, ref pasos);   // caso recursivo: mitad derecha
            return BuscarBinario(exps, codigo, izq, medio - 1, ref pasos);              // caso recursivo: mitad izquierda
        }

        // ¿La lista está ordenada por código? (recursiva)
        public static bool EstaOrdenadoPorCodigo(Expediente[] exps, int cantidad, int k = 0)
        {
            if (k >= cantidad - 1) return true;                   // caso base: no quedan pares por revisar
            if (string.Compare(exps[k].Codigo, exps[k + 1].Codigo, StringComparison.Ordinal) > 0)
                return false;                                     // caso base: hay un par desordenado
            return EstaOrdenadoPorCodigo(exps, cantidad, k + 1);  // caso recursivo
        }

        // Compara dos expedientes. criterio: 1 = código, 2 = apellidos y nombres.
        public static int Comparar(Expediente a, Expediente b, int criterio)
        {
            if (criterio == 1)
                return string.Compare(a.Codigo, b.Codigo, StringComparison.Ordinal);
            return string.Compare(a.Apellidos + " " + a.Nombres, b.Apellidos + " " + b.Nombres,
                                  StringComparison.InvariantCultureIgnoreCase);
        }

        // Ordenamiento burbuja. Cuenta las comparaciones y los intercambios.
        public static void Ordenar(Expediente[] exps, int cantidad, int criterio,
                                   out int comparaciones, out int intercambios)
        {
            comparaciones = 0;
            intercambios = 0;
            for (int i = 0; i < cantidad - 1; i++)
            {
                for (int j = 0; j < cantidad - i - 1; j++)
                {
                    comparaciones++;
                    if (Comparar(exps[j], exps[j + 1], criterio) > 0)
                    {
                        Expediente aux = exps[j];
                        exps[j] = exps[j + 1];
                        exps[j + 1] = aux;
                        intercambios++;
                    }
                }
            }
        }

        // Elimina la posición pos corriendo los elementos hacia atrás.
        public static void Eliminar(Expediente[] exps, ref int cantidad, int pos)
        {
            for (int k = pos; k < cantidad - 1; k++)
                exps[k] = exps[k + 1];
            exps[cantidad - 1] = null;
            cantidad--;
        }

        // Arreglo bidimensional: tabla[fila, columna] = cantidad de expedientes de un tipo de
        // trámite (fila) con un estado (columna). Las columnas siguen el orden de Expediente.Estados.
        // 'tramites' (de tamaño >= cantidad) recibe el nombre de cada fila; 'filas' dice cuántas hay.
        public static int[,] Resumir(Expediente[] exps, int cantidad, string[] tramites, out int filas)
        {
            int[,] tabla = new int[cantidad, Expediente.Estados.Length];
            filas = 0;
            for (int i = 0; i < cantidad; i++)
            {
                string tramite = exps[i].TipoTramite.ToUpper();
                int f = Array.IndexOf(tramites, tramite, 0, filas);   // ¿ya tiene fila este trámite?
                if (f == -1)
                {
                    f = filas;
                    tramites[filas] = tramite;
                    filas++;
                }
                int c = Array.IndexOf(Expediente.Estados, exps[i].Estado);
                tabla[f, c]++;
            }
            return tabla;
        }
    }
}
