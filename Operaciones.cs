namespace MesaDePartes
{
    public static class Operaciones
    {
        // ---------- MOSTRAR UN EXPEDIENTE ----------
        public static void MostrarExpediente(int i)
        {
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Código:        " + Datos.codigos[i]);
            Console.WriteLine("DNI:           " + Datos.dnis[i]);
            Console.WriteLine("Apellidos:     " + Datos.apellidos[i]);
            Console.WriteLine("Nombres:       " + Datos.nombres[i]);
            Console.WriteLine("Grado:         " + Datos.grados[i]);
            Console.WriteLine("Trámite:       " + Datos.tiposTramite[i]);
            Console.WriteLine("Asunto:        " + Datos.asuntos[i]);
            Console.WriteLine("Fecha:         " + Datos.fechas[i]);
            Console.WriteLine("Estado:        " + Datos.estados[i]);
            Console.WriteLine("------------------------------------");
        }

        // ---------- 1. REGISTRAR ----------
        public static void RegistrarExpediente()
        {
            if (Datos.total >= Datos.Capacidad)
            {
                Console.WriteLine("Error: Memoria llena, no se pueden registrar más expedientes.");
                return;
            }

            string cod = Entrada.PedirCodigo();

            string dni = Entrada.PedirDNI();
            string ape = Entrada.PedirNombre("Ingrese Apellidos (Ej: Alaya Pérez):");
            string nom = Entrada.PedirNombre("Ingrese Nombres (Ej: Daniel Luis):");
            string gra = Entrada.PedirTexto("Ingrese Grado y Sección (Ej: 3ro A):", 2);
            string tip = Entrada.PedirTexto("Ingrese Tipo de Trámite:", 3);
            string asu = Entrada.PedirTexto("Ingrese Asunto de la solicitud:", 5);
            string fec = Entrada.PedirFecha();

            int t = Datos.total;
            Datos.codigos[t] = cod;
            Datos.dnis[t] = dni;
            Datos.apellidos[t] = ape;
            Datos.nombres[t] = nom;
            Datos.grados[t] = gra;
            Datos.tiposTramite[t] = tip;
            Datos.asuntos[t] = asu;
            Datos.fechas[t] = fec;
            Datos.estados[t] = "Recibido";   // estado inicial
            Datos.total++;

            Console.WriteLine("¡Éxito! Expediente [" + cod + "] guardado correctamente.");
        }

        // ---------- 2. BUSCAR (BÚSQUEDA LINEAL) ----------
        public static void BuscarExpediente()
        {
            if (Datos.total == 0)
            {
                Console.WriteLine("No hay expedientes registrados para buscar.");
                return;
            }

            Console.WriteLine("Buscar por: 1. Código   2. Apellido");
            int tipo = Entrada.LeerOpcion();
            if (tipo == 1) BuscarPorCodigo();
            else if (tipo == 2) BuscarPorApellido();
            else Console.WriteLine("Opción no válida.");
        }

        private static void BuscarPorCodigo()
        {
            Console.WriteLine("Ingrese el código que desea buscar (Ej: EXP-001):");
            string buscado = (Console.ReadLine() ?? "").Trim().ToUpper();
            int pos = Datos.BuscarIndice(buscado);
            if (pos == -1)
                Console.WriteLine("No se encontró ningún expediente con el código: " + buscado);
            else
                MostrarExpediente(pos);
        }

        private static void BuscarPorApellido()
        {
            Console.WriteLine("Ingrese el apellido que desea buscar (Ej: Alaya):");
            string buscado = (Console.ReadLine() ?? "").Trim().ToUpper();
            int n = buscado.Length;
            if (n == 0)
            {
                Console.WriteLine("ERROR: Debe escribir un apellido para buscar.");
                return;
            }

            int encontrados = 0;
            for (int k = 0; k < Datos.total; k++)
            {
                string apMay = Datos.apellidos[k].ToUpper();
                if (apMay.Length >= n && apMay.Substring(0, n) == buscado)
                {
                    encontrados++;
                    Console.WriteLine("   ¡COINCIDENCIA " + encontrados + "!");
                    MostrarExpediente(k);
                }
            }

            if (encontrados == 0)
                Console.WriteLine("No se encontró ningún expediente con el apellido: " + buscado);
            else
                Console.WriteLine("Expedientes encontrados: " + encontrados);
        }

        // ---------- CLAVE DE COMPARACIÓN: APELLIDO (y nombres si hay empate) ----------
        private static string ClaveOrden(int k)
        {
            return (Datos.apellidos[k] + " " + Datos.nombres[k]).ToUpper();
        }

        // ---------- MUESTRA LA LISTA EN UNA LÍNEA ----------
        private static void MostrarOrden()
        {
            for (int k = 0; k < Datos.total; k++)
            {
                Console.Write("[" + Datos.codigos[k] + " " + Datos.apellidos[k] + ", "
                    + Datos.nombres[k] + " - " + Datos.dnis[k] + "] ");
            }
            Console.WriteLine();
        }

        // ---------- 3. ORDENAR POR APELLIDO (BURBUJA CON SEGUIMIENTO) ----------
        public static void OrdenarExpedientes()
        {
            if (Datos.total <= 1)
            {
                Console.WriteLine("No hay suficientes expedientes para ordenar.");
                return;
            }

            int comparaciones = 0;
            int intercambios = 0;

            Console.WriteLine();
            Console.WriteLine("====== MÉTODO BURBUJA: ORDEN ALFABÉTICO POR APELLIDO ======");
            Console.WriteLine("Lista ANTES de ordenar:");
            MostrarOrden();
            Console.WriteLine();

            for (int i = 0; i <= Datos.total - 2; i++)
            {
                int cambiosPasada = 0;
                for (int j = 0; j <= Datos.total - i - 2; j++)
                {
                    comparaciones++;
                    if (string.Compare(ClaveOrden(j), ClaveOrden(j + 1), StringComparison.Ordinal) > 0)
                    {
                        // Se intercambian TODOS los arreglos para no desalinear los datos.
                        Datos.Intercambiar(Datos.codigos, j);
                        Datos.Intercambiar(Datos.dnis, j);
                        Datos.Intercambiar(Datos.apellidos, j);
                        Datos.Intercambiar(Datos.nombres, j);
                        Datos.Intercambiar(Datos.grados, j);
                        Datos.Intercambiar(Datos.tiposTramite, j);
                        Datos.Intercambiar(Datos.asuntos, j);
                        Datos.Intercambiar(Datos.fechas, j);
                        Datos.Intercambiar(Datos.estados, j);
                        intercambios++;
                        cambiosPasada++;
                    }
                }
                Console.WriteLine("Pasada " + (i + 1) + " (" + cambiosPasada + " intercambios):");
                MostrarOrden();
            }

            Console.WriteLine();
            Console.WriteLine("Lista DESPUÉS de ordenar:");
            MostrarOrden();
            Console.WriteLine("Comparaciones realizadas: " + comparaciones);
            Console.WriteLine("Intercambios realizados:  " + intercambios);
            if (intercambios == 0)
                Console.WriteLine("(La lista ya estaba ordenada por apellido.)");
            Console.WriteLine("============================================================");
        }

        // ---------- 4. LISTAR ----------
        public static void ListarExpedientes()
        {
            if (Datos.total == 0)
            {
                Console.WriteLine("La lista de expedientes está vacía.");
                return;
            }

            Console.WriteLine("==================================================");
            Console.WriteLine("         LISTADO GENERAL DE EXPEDIENTES");
            Console.WriteLine("==================================================");
            for (int i = 0; i < Datos.total; i++)
            {
                Console.WriteLine("[" + (i + 1) + "] " + Datos.codigos[i] + " | DNI: " + Datos.dnis[i]
                    + " | " + Datos.apellidos[i] + ", " + Datos.nombres[i]
                    + " | " + Datos.tiposTramite[i] + " | " + Datos.estados[i]);
            }
            Console.WriteLine("Total de expedientes: " + Datos.total);
        }

        // ---------- 5. MODIFICAR ----------
        public static void ModificarExpediente()
        {
            if (Datos.total == 0)
            {
                Console.WriteLine("No hay expedientes registrados para modificar.");
                return;
            }

            Console.WriteLine("Ingrese el código del expediente a modificar (Ej: EXP-001):");
            string buscado = (Console.ReadLine() ?? "").Trim().ToUpper();
            int pos = Datos.BuscarIndice(buscado);
            if (pos == -1)
            {
                Console.WriteLine("No se encontró ningún expediente con el código: " + buscado);
                return;
            }

            int campo;
            do
            {
                Console.WriteLine("Datos actuales:");
                MostrarExpediente(pos);
                Console.WriteLine("¿Qué desea modificar?");
                Console.WriteLine("1. DNI");
                Console.WriteLine("2. Apellidos");
                Console.WriteLine("3. Nombres");
                Console.WriteLine("4. Grado y Sección");
                Console.WriteLine("5. Tipo de Trámite");
                Console.WriteLine("6. Fecha de Recepción");
                Console.WriteLine("7. Asunto");
                Console.WriteLine("8. Terminar modificación");
                campo = Entrada.LeerOpcion();

                switch (campo)
                {
                    case 1:
                        Datos.dnis[pos] = Entrada.PedirDNI();
                        Console.WriteLine("DNI actualizado.");
                        break;
                    case 2:
                        Datos.apellidos[pos] = Entrada.PedirNombre("Ingrese nuevos Apellidos:");
                        Console.WriteLine("Apellidos actualizados.");
                        break;
                    case 3:
                        Datos.nombres[pos] = Entrada.PedirNombre("Ingrese nuevos Nombres:");
                        Console.WriteLine("Nombres actualizados.");
                        break;
                    case 4:
                        Datos.grados[pos] = Entrada.PedirTexto("Ingrese nuevo Grado y Sección (Ej: 3ro A):", 2);
                        Console.WriteLine("Grado y Sección actualizados.");
                        break;
                    case 5:
                        Datos.tiposTramite[pos] = Entrada.PedirTexto("Ingrese nuevo Tipo de Trámite:", 3);
                        Console.WriteLine("Tipo de trámite actualizado.");
                        break;
                    case 6:
                        Datos.fechas[pos] = Entrada.PedirFecha();
                        Console.WriteLine("Fecha actualizada.");
                        break;
                    case 7:
                        Datos.asuntos[pos] = Entrada.PedirTexto("Ingrese nuevo Asunto:", 5);
                        Console.WriteLine("Asunto actualizado.");
                        break;
                    case 8:
                        Console.WriteLine("Modificación finalizada.");
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            } while (campo != 8);
        }

        // ---------- 6. ELIMINAR ----------
        public static void EliminarExpediente()
        {
            if (Datos.total == 0)
            {
                Console.WriteLine("No hay expedientes registrados para eliminar.");
                return;
            }

            Console.WriteLine("Ingrese el código del expediente a eliminar (Ej: EXP-001):");
            string buscado = (Console.ReadLine() ?? "").Trim().ToUpper();
            int pos = Datos.BuscarIndice(buscado);
            if (pos == -1)
            {
                Console.WriteLine("No se encontró ningún expediente con el código: " + buscado);
                return;
            }

            MostrarExpediente(pos);
            Console.WriteLine("¿Está seguro de eliminar este expediente? (S/N):");
            string confirma = (Console.ReadLine() ?? "").Trim().ToUpper();

            if (confirma == "S")
            {
                // Se corren los datos una posición hacia atrás para tapar el hueco.
                for (int k = pos; k <= Datos.total - 2; k++)
                {
                    Datos.codigos[k] = Datos.codigos[k + 1];
                    Datos.dnis[k] = Datos.dnis[k + 1];
                    Datos.apellidos[k] = Datos.apellidos[k + 1];
                    Datos.nombres[k] = Datos.nombres[k + 1];
                    Datos.grados[k] = Datos.grados[k + 1];
                    Datos.tiposTramite[k] = Datos.tiposTramite[k + 1];
                    Datos.asuntos[k] = Datos.asuntos[k + 1];
                    Datos.fechas[k] = Datos.fechas[k + 1];
                    Datos.estados[k] = Datos.estados[k + 1];
                }
                Datos.total--;
                Console.WriteLine("Expediente eliminado correctamente.");
            }
            else
            {
                Console.WriteLine("Eliminación cancelada.");
            }
        }
    }
}
