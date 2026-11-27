namespace MesaDePartes
{
    // Flujo de cada opción del menú: pide los datos (Entrada), usa la lógica (clsExpedientes)
    // y muestra el resultado. Las que pueden cambiar los datos devuelven true para que
    // Program guarde el archivo.
    public static class Operaciones
    {
        // ---------- 1. REGISTRAR ----------
        public static bool Registrar(ref Expediente[] exps, ref int cantidad)
        {
            string cod = Entrada.PedirCodigo(exps, cantidad);
            string dni = Entrada.PedirDNI();
            string ape = Entrada.PedirNombre("Ingrese Apellidos (Ej: Alaya Pérez):");
            string nom = Entrada.PedirNombre("Ingrese Nombres (Ej: Daniel Luis):");
            string gra = Entrada.PedirTexto("Ingrese Grado y Sección (Ej: 3ro A):", Validaciones.MinGrado);
            string tip = Entrada.PedirTexto("Ingrese Tipo de Trámite:", Validaciones.MinTramite);
            string asu = Entrada.PedirTexto("Ingrese Asunto de la solicitud:", Validaciones.MinAsunto);
            string fec = Entrada.PedirFecha();

            clsExpedientes.Agregar(ref exps, ref cantidad,
                new Expediente(cod, dni, ape, nom, gra, tip, asu, fec, Expediente.Estados[0]));
            Console.WriteLine("¡Éxito! Expediente [" + cod + "] registrado.");
            return true;
        }

        // ---------- 2. BUSCAR ----------
        public static void Buscar(Expediente[] exps, int cantidad)
        {
            Console.WriteLine("Buscar por: 1. Código   2. Apellido   3. Texto del asunto");
            int tipo = Entrada.LeerOpcion();
            if (tipo == 1) BuscarPorCodigo(exps, cantidad);
            else if (tipo == 2 || tipo == 3) BuscarPorTexto(exps, cantidad, tipo == 2);
            else Console.WriteLine("Opción no válida.");
        }

        private static void BuscarPorCodigo(Expediente[] exps, int cantidad)
        {
            string codigo = Entrada.Leer("Ingrese el código a buscar (Ej: EXP-001):").ToUpper();

            int pos = clsExpedientes.BuscarIndice(exps, cantidad, codigo);
            Console.WriteLine("Búsqueda lineal: " + (pos == -1 ? cantidad : pos + 1) + " comparaciones.");

            // Si la lista está ordenada por código, también se prueba la búsqueda binaria.
            if (clsExpedientes.EstaOrdenadoPorCodigo(exps, cantidad))
            {
                int pasos = 0;
                clsExpedientes.BuscarBinario(exps, codigo, 0, cantidad - 1, ref pasos);
                Console.WriteLine("Búsqueda binaria: " + pasos + " comparaciones (lista ordenada por código).");
            }
            else
                Console.WriteLine("(Ordene por código con la opción 3 para comparar con la búsqueda binaria.)");

            if (pos == -1) Console.WriteLine("No se encontró el código: " + codigo);
            else Console.WriteLine(exps[pos].ToDetalle());
        }

        // Busca por el inicio del apellido (porApellido = true) o por un texto dentro del asunto.
        private static void BuscarPorTexto(Expediente[] exps, int cantidad, bool porApellido)
        {
            string texto = Entrada.Leer(porApellido ? "Ingrese el apellido a buscar (Ej: Alaya):"
                                                    : "Ingrese el texto a buscar en el asunto:");
            int encontrados = 0;
            for (int k = 0; k < cantidad && texto != ""; k++)
            {
                bool coincide = porApellido
                    ? exps[k].Apellidos.StartsWith(texto, StringComparison.OrdinalIgnoreCase)
                    : exps[k].Asunto.Contains(texto, StringComparison.OrdinalIgnoreCase);
                if (coincide)
                {
                    encontrados++;
                    Console.WriteLine(exps[k].ToDetalle());
                }
            }
            Console.WriteLine(encontrados == 0 ? "No se encontraron coincidencias."
                                               : "Expedientes encontrados: " + encontrados);
        }

        // ---------- 3. ORDENAR ----------
        // Devuelve true si el orden cambió (hay que guardar).
        public static bool Ordenar(Expediente[] exps, int cantidad)
        {
            if (cantidad < 2)
            {
                Console.WriteLine("Se necesitan al menos 2 expedientes para ordenar.");
                return false;
            }

            Console.WriteLine("Ordenar por: 1. Código   2. Apellido");
            int criterio = Entrada.LeerOpcion();
            if (criterio != 1 && criterio != 2)
            {
                Console.WriteLine("Opción no válida.");
                return false;
            }

            int comparaciones, intercambios;
            clsExpedientes.Ordenar(exps, cantidad, criterio, out comparaciones, out intercambios);

            Listar(exps, cantidad);
            Console.WriteLine("Método burbuja -> comparaciones: " + comparaciones + " | intercambios: " + intercambios);
            return intercambios > 0;
        }

        // ---------- 4. LISTAR ----------
        // Recursiva: cada llamada muestra el expediente k y se llama con k + 1.
        public static void Listar(Expediente[] exps, int cantidad, int k = 0)
        {
            if (k == 0)
                Console.WriteLine("=== LISTADO DE EXPEDIENTES ===");
            if (k >= cantidad)                                    // caso base: no quedan expedientes
            {
                Console.WriteLine("Total de expedientes: " + cantidad);
                return;
            }
            Console.WriteLine("[" + (k + 1) + "] " + exps[k]);
            Listar(exps, cantidad, k + 1);                        // caso recursivo
        }

        // Pide un código y devuelve su posición (o -1 si no existe).
        private static int PedirExistente(Expediente[] exps, int cantidad, string accion)
        {
            string codigo = Entrada.Leer("Ingrese el código del expediente a " + accion + ":").ToUpper();
            int pos = clsExpedientes.BuscarIndice(exps, cantidad, codigo);
            if (pos == -1)
                Console.WriteLine("No se encontró el código: " + codigo);
            return pos;
        }

        // ---------- 5. MODIFICAR (un campo por vez) ----------
        public static bool Modificar(Expediente[] exps, int cantidad)
        {
            int pos = PedirExistente(exps, cantidad, "modificar");
            if (pos == -1) return false;

            Expediente e = exps[pos];
            Console.WriteLine(e.ToDetalle());
            Console.WriteLine("¿Qué desea modificar? 1. DNI  2. Apellidos  3. Nombres  4. Grado  5. Trámite  6. Fecha  7. Asunto");
            switch (Entrada.LeerOpcion())
            {
                case 1: e.Dni = Entrada.PedirDNI(); break;
                case 2: e.Apellidos = Entrada.PedirNombre("Nuevos Apellidos:"); break;
                case 3: e.Nombres = Entrada.PedirNombre("Nuevos Nombres:"); break;
                case 4: e.Grado = Entrada.PedirTexto("Nuevo Grado y Sección:", Validaciones.MinGrado); break;
                case 5: e.TipoTramite = Entrada.PedirTexto("Nuevo Tipo de Trámite:", Validaciones.MinTramite); break;
                case 6: e.Fecha = Entrada.PedirFecha(); break;
                case 7: e.Asunto = Entrada.PedirTexto("Nuevo Asunto:", Validaciones.MinAsunto); break;
                default:
                    Console.WriteLine("Opción no válida.");
                    return false;
            }
            Console.WriteLine("Expediente actualizado.");
            return true;
        }

        // ---------- 6. CAMBIAR ESTADO ----------
        public static bool CambiarEstado(Expediente[] exps, int cantidad)
        {
            int pos = PedirExistente(exps, cantidad, "actualizar");
            if (pos == -1) return false;

            Console.WriteLine("Estado actual: " + exps[pos].Estado);
            for (int k = 0; k < Expediente.Estados.Length; k++)
                Console.WriteLine((k + 1) + ". " + Expediente.Estados[k]);

            int op = Entrada.LeerOpcion();
            if (op < 1 || op > Expediente.Estados.Length)
            {
                Console.WriteLine("Opción no válida.");
                return false;
            }
            exps[pos].Estado = Expediente.Estados[op - 1];
            Console.WriteLine("Estado actualizado a: " + exps[pos].Estado);
            return true;
        }

        // ---------- 7. ELIMINAR ----------
        public static bool Eliminar(Expediente[] exps, ref int cantidad)
        {
            int pos = PedirExistente(exps, cantidad, "eliminar");
            if (pos == -1) return false;

            Console.WriteLine(exps[pos].ToDetalle());
            if (Entrada.Leer("¿Está seguro de eliminarlo? (S/N):").ToUpper() != "S")
            {
                Console.WriteLine("Eliminación cancelada.");
                return false;
            }
            clsExpedientes.Eliminar(exps, ref cantidad, pos);
            Console.WriteLine("Expediente eliminado correctamente.");
            return true;
        }

        // ---------- 10. RESUMEN (arreglo bidimensional) ----------
        public static void Resumen(Expediente[] exps, int cantidad)
        {
            string[] tramites = new string[cantidad];
            int filas;
            int[,] tabla = clsExpedientes.Resumir(exps, cantidad, tramites, out filas);
            int columnas = Expediente.Estados.Length;
            int[] totalColumna = new int[columnas];

            Console.Write("Trámite".PadRight(22));
            foreach (string estado in Expediente.Estados)
                Console.Write(estado.PadRight(12));
            Console.WriteLine("Total");

            for (int f = 0; f < filas; f++)
            {
                string nombre = tramites[f].Length > 21 ? tramites[f].Substring(0, 21) : tramites[f];
                Console.Write(nombre.PadRight(22));
                int totalFila = 0;
                for (int c = 0; c < columnas; c++)
                {
                    Console.Write(tabla[f, c].ToString().PadRight(12));
                    totalFila += tabla[f, c];
                    totalColumna[c] += tabla[f, c];
                }
                Console.WriteLine(totalFila);
            }

            Console.Write("TOTAL".PadRight(22));
            for (int c = 0; c < columnas; c++)
                Console.Write(totalColumna[c].ToString().PadRight(12));
            Console.WriteLine(cantidad);
        }
    }
}
