namespace MesaDePartes
{
    // FLUJOS DE MENÚ: piden datos (Entrada), llaman a la lógica (clsExpedientes)
    // y muestran resultados.
    public static class Operaciones
    {
        // ---------- 1. REGISTRAR ----------
        public static void RegistrarExpediente(ref Expediente[] exps, ref int cantidad)
        {
            string cod = Entrada.PedirCodigo(exps, cantidad);
            string dni = Entrada.PedirDNI();
            string ape = Entrada.PedirNombre("Ingrese Apellidos (Ej: Alaya Pérez):");
            string nom = Entrada.PedirNombre("Ingrese Nombres (Ej: Daniel Luis):");
            string gra = Entrada.PedirTexto("Ingrese Grado y Sección (Ej: 3ro A):", 2);
            string tip = Entrada.PedirTexto("Ingrese Tipo de Trámite:", 3);
            string asu = Entrada.PedirTexto("Ingrese Asunto de la solicitud:", 5);
            string fec = Entrada.PedirFecha();
 
            Expediente nuevo = new Expediente(cod, dni, ape, nom, gra, tip, asu, fec, Expediente.Estados[0]);
            clsExpedientes.Agregar(ref exps, ref cantidad, nuevo);
 
            Console.WriteLine("¡Éxito! Expediente [" + cod + "] registrado.");
        }
 
        // ---------- 2. BUSCAR ----------
        public static void BuscarExpediente(Expediente[] exps, int cantidad)
        {
            if (cantidad == 0)
            {
                Console.WriteLine("No hay expedientes registrados para buscar.");
                return;
            }
 
             Console.WriteLine("Buscar por: 1. Código   2. Apellido   3. Texto del asunto");
            int tipo = Entrada.LeerOpcion();
            if (tipo == 1) BuscarPorCodigo(exps, cantidad);
            else if (tipo == 2) BuscarPorApellido(exps, cantidad);
            else if (tipo == 3) BuscarPorAsunto(exps, cantidad);
            else Console.WriteLine("Opción no válida.");
        }
 
        private static void BuscarPorCodigo(Expediente[] exps, int cantidad)
        {
            string buscado = Entrada.PedirBusqueda("Ingrese el código que desea buscar (Ej: EXP-001):").ToUpper();
            int pos = clsExpedientes.BuscarIndice(exps, cantidad, buscado);
            if (pos == -1)
                Console.WriteLine("No se encontró ningún expediente con el código: " + buscado);
            else
                Console.WriteLine(exps[pos].ToDetalle());
        }
 
        private static void BuscarPorApellido(Expediente[] exps, int cantidad)
        {
            string buscado = Entrada.PedirBusqueda("Ingrese el apellido que desea buscar (Ej: Alaya):").ToUpper();
            int n = buscado.Length;
            if (n == 0)
            {
                Console.WriteLine("ERROR: Debe escribir un apellido para buscar.");
                return;
            }
 
            int encontrados = 0;
            for (int k = 0; k < cantidad; k++)
            {
                string apMay = exps[k].Apellidos.ToUpper();
                if (apMay.Length >= n && apMay.Substring(0, n) == buscado)
                {
                    encontrados++;
                    MostrarCoincidencia(exps[k], encontrados);
                }
            }
            MostrarResumenBusqueda(encontrados, "No se encontró ningún expediente con el apellido: " + buscado);
                    }
 
        // Busca un texto dentro del asunto (IndexOf, sin distinguir mayúsculas).
        private static void BuscarPorAsunto(Expediente[] exps, int cantidad)
        {
            string buscado = Entrada.PedirBusqueda("Ingrese el texto a buscar en el asunto:");
            if (buscado.Length == 0)
            {
                Console.WriteLine("ERROR: Debe escribir un texto para buscar.");
                return;
            }
 
            int encontrados = 0;
            for (int k = 0; k < cantidad; k++)
            {
                if (exps[k].Asunto.IndexOf(buscado, StringComparison.OrdinalIgnoreCase) != -1)
                {
                    encontrados++;
                    MostrarCoincidencia(exps[k], encontrados);
                }
            }
            MostrarResumenBusqueda(encontrados, "No se encontró ningún asunto que contenga: " + buscado);

        }
 
        // Muestra un expediente encontrado en una búsqueda por apellido o por asunto.
        private static void MostrarCoincidencia(Expediente e, int numero)
        {
            Console.WriteLine("   ¡COINCIDENCIA " + numero + "!");
            Console.WriteLine(e.ToDetalle());
        }
 
        private static void MostrarResumenBusqueda(int encontrados, string mensajeNoEncontrado)
        {
            if (encontrados == 0)
                Console.WriteLine(mensajeNoEncontrado);
            else
                Console.WriteLine("Expedientes encontrados: " + encontrados);
        }
 
        // ---------- 3. ORDENAR (BURBUJA CON SEGUIMIENTO) ----------
        private static void MostrarOrden(Expediente[] exps, int cantidad)
        {
            for (int k = 0; k < cantidad; k++)
            {
                Console.Write("[" + exps[k].Codigo + " " + exps[k].Apellidos + ", "
                    + exps[k].Nombres + " - " + exps[k].Dni + "] ");
            }
            Console.WriteLine();
        }
 
        public static void OrdenarExpedientes(Expediente[] exps, int cantidad)
        {
            if (cantidad <= 1)
            {
                Console.WriteLine("No hay suficientes expedientes para ordenar.");
                return;
            }
                        Console.WriteLine("Ordenar por: 1. Código   2. Apellido");
            int criterio = Entrada.LeerOpcion();
            if (criterio != 1 && criterio != 2)
            {
                Console.WriteLine("Opción no válida.");
                return;
            }
 
            int comparaciones = 0;
            int intercambios = 0;
 
            Console.WriteLine();
            Console.WriteLine("====== MÉTODO BURBUJA: ORDEN POR " + (criterio == 1 ? "CÓDIGO" : "APELLIDO") + " ======");
            Console.WriteLine("Lista ANTES de ordenar:");
            MostrarOrden(exps, cantidad);
            Console.WriteLine();
 
            for (int i = 0; i <= cantidad - 2; i++)
            {
            int cambiosPasada = clsExpedientes.PasadaBurbuja(exps, cantidad, i, criterio,
                                                                 ref comparaciones, ref intercambios);

                Console.WriteLine("Pasada " + (i + 1) + " (" + cambiosPasada + " intercambios):");
                MostrarOrden(exps, cantidad);
            }
 
            Console.WriteLine();
            Console.WriteLine("Lista DESPUÉS de ordenar:");
            MostrarOrden(exps, cantidad);
            Console.WriteLine("Comparaciones realizadas: " + comparaciones);
            Console.WriteLine("Intercambios realizados:  " + intercambios);
            if (intercambios == 0)
            Console.WriteLine("(La lista ya estaba ordenada.)");
            Console.WriteLine("============================================================");
        }
 
        // ---------- 4. LISTAR ----------
        public static void ListarExpedientes(Expediente[] exps, int cantidad)
        {
            if (cantidad == 0)
            {
                Console.WriteLine("La lista de expedientes está vacía.");
                return;
            }
 
            Console.WriteLine("==================================================");
            Console.WriteLine("         LISTADO GENERAL DE EXPEDIENTES");
            Console.WriteLine("==================================================");
            for (int i = 0; i < cantidad; i++)
                Console.WriteLine("[" + (i + 1) + "] " + exps[i].ToString());
            Console.WriteLine("Total de expedientes: " + cantidad);
        }
 
        // Pide un código existente. Devuelve la posición o -1 si no existe.
        private static int PedirExistente(Expediente[] exps, int cantidad, string accion)
        {
            string buscado = Entrada.PedirBusqueda("Ingrese el código del expediente a " + accion + " (Ej: EXP-001):").ToUpper();
            
            int pos = clsExpedientes.BuscarIndice(exps, cantidad, buscado);
                        int compLineal = (pos == -1) ? cantidad : pos + 1;
            Console.WriteLine("Búsqueda lineal: " + compLineal + " comparaciones.");
 
            // Si la lista está ordenada por código, también se prueba la búsqueda binaria.
            if (clsExpedientes.EstaOrdenadoPorCodigo(exps, cantidad))
            {
                int compBinaria;
                clsExpedientes.BuscarBinarioIterativo(exps, cantidad, buscado, out compBinaria);
                Console.WriteLine("Búsqueda binaria: " + compBinaria + " comparaciones (lista ordenada por código).");
            }
            else
            {
                Console.WriteLine("(Para comparar con la búsqueda binaria, ordene primero por código: opción 3.)");
            }
 
            if (pos == -1)
                Console.WriteLine("No se encontró ningún expediente con el código: " + buscado);
            return pos;
        }
 
        // ---------- 5. MODIFICAR ----------
        public static void ModificarExpediente(Expediente[] exps, int cantidad)
        {
            if (cantidad == 0)
            {
                Console.WriteLine("No hay expedientes registrados para modificar.");
                return;
            }
 
            int pos = PedirExistente(exps, cantidad, "modificar");
            if (pos == -1) return;
 
            Expediente e = exps[pos];
            int campo;
            do
            {
                Console.WriteLine("Datos actuales:");
                Console.WriteLine(e.ToDetalle());
                MostrarMenuModificar();
                campo = Entrada.LeerOpcion();
 
                ModificarCampo(e, campo);
            } while (campo != 8);
        }
 
        private static void MostrarMenuModificar()
        {
            Console.WriteLine("¿Qué desea modificar?");
            Console.WriteLine("1. DNI");
            Console.WriteLine("2. Apellidos");
            Console.WriteLine("3. Nombres");
            Console.WriteLine("4. Grado y Sección");
            Console.WriteLine("5. Tipo de Trámite");
            Console.WriteLine("6. Fecha de Recepción");
            Console.WriteLine("7. Asunto");
            Console.WriteLine("8. Terminar modificación");
        }
 
        // Pide el nuevo valor del campo elegido. Devuelve true si se modificó algo.
        private static bool ModificarCampo(Expediente e, int campo)
        {
            switch (campo)
            {
                case 1:
                    e.Dni = Entrada.PedirDNI();
                    Console.WriteLine("DNI actualizado.");
                    return true;
                case 2:
                    e.Apellidos = Entrada.PedirNombre("Ingrese nuevos Apellidos:");
                    Console.WriteLine("Apellidos actualizados.");
                    return true;
                case 3:
                    e.Nombres = Entrada.PedirNombre("Ingrese nuevos Nombres:");
                    Console.WriteLine("Nombres actualizados.");
                    return true;
                case 4:
                    e.Grado = Entrada.PedirTexto("Ingrese nuevo Grado y Sección (Ej: 3ro A):", 2);
                    Console.WriteLine("Grado y Sección actualizados.");
                    return true;
                case 5:
                    e.TipoTramite = Entrada.PedirTexto("Ingrese nuevo Tipo de Trámite:", 3);
                    Console.WriteLine("Tipo de trámite actualizado.");
                    return true;
                case 6:
                    e.Fecha = Entrada.PedirFecha();
                    Console.WriteLine("Fecha actualizada.");
                    return true;
                case 7:
                    e.Asunto = Entrada.PedirTexto("Ingrese nuevo Asunto:", 5);
                    Console.WriteLine("Asunto actualizado.");
                    return true;
                case 8:
                    Console.WriteLine("Modificación finalizada.");
                    return false;
                default:
                    Console.WriteLine("Opción no válida.");
                    return false;
            }
        }
 
        // ---------- 6. CAMBIAR ESTADO ----------
        public static void CambiarEstado(Expediente[] exps, int cantidad)
        {
            if (cantidad == 0)
            {
                Console.WriteLine("No hay expedientes registrados.");
                return;
            }
 
            int pos = PedirExistente(exps, cantidad, "actualizar");
            if (pos == -1) return;
 
            Console.WriteLine("Estado actual: " + exps[pos].Estado);
            Console.WriteLine("Nuevo estado:");
            for (int k = 0; k < Expediente.Estados.Length; k++)
                Console.WriteLine((k + 1) + ". " + Expediente.Estados[k]);
 
            int op = Entrada.LeerOpcion();
            if (op < 1 || op > Expediente.Estados.Length)
            {
                Console.WriteLine("Opción no válida.");
                return;
            }
 
            exps[pos].Estado = Expediente.Estados[op - 1];
            Console.WriteLine("Estado actualizado a: " + exps[pos].Estado);
        }
 
        // ---------- 7. ELIMINAR ----------

        public static void EliminarExpediente(Expediente[] exps, ref int cantidad)
        {
            if (cantidad == 0)
            {
                Console.WriteLine("No hay expedientes registrados para eliminar.");
                return;
            }
 
            int pos = PedirExistente(exps, cantidad, "eliminar");
            if (pos == -1) return;
 
            Console.WriteLine(exps[pos].ToDetalle());
            string confirma = Entrada.PedirBusqueda("¿Está seguro de eliminar este expediente? (S/N):").ToUpper();
 
            if (confirma == "S")
            {
                clsExpedientes.Eliminar(exps, ref cantidad, pos);
                Console.WriteLine("Expediente eliminado correctamente.");
            }
 
            Console.WriteLine("Eliminación cancelada.");
            return;
        }
    }
}
