using System.Text;

namespace MesaDePartes
{
    public class Program
    {
        private const string NombreInstitucion = "I.E. N.° 81770";

        public static void Main()
        {
            Console.OutputEncoding = new UTF8Encoding(false);   // para ver tildes y ñ
            Console.InputEncoding = new UTF8Encoding(false);

            Expediente[] expedientes = new Expediente[clsExpedientes.CapacidadInicial];
            int cantidad = 0;

            CargarDatos(ref expedientes, ref cantidad);

            int opc;
            do
            {
                opc = MostrarMenu();
                bool huboCambios = false;       // si es true, se guarda el archivo al terminar la opción

                // Estas opciones necesitan al menos un expediente registrado.
                if (cantidad == 0 && ((opc >= 2 && opc <= 7) || opc == 10))
                {
                    Console.WriteLine("No hay expedientes registrados.");
                    continue;
                }

                switch (opc)
                {
                    case 1: huboCambios = Operaciones.Registrar(ref expedientes, ref cantidad); break;
                    case 2: Operaciones.Buscar(expedientes, cantidad); break;
                    case 3: huboCambios = Operaciones.Ordenar(expedientes, cantidad); break;
                    case 4: Operaciones.Listar(expedientes, cantidad); break;
                    case 5: huboCambios = Operaciones.Modificar(expedientes, cantidad); break;
                    case 6: huboCambios = Operaciones.CambiarEstado(expedientes, cantidad); break;
                    case 7: huboCambios = Operaciones.Eliminar(expedientes, ref cantidad); break;
                    case 8: huboCambios = true; break;                       // guardar ahora
                    case 9: CargarDatos(ref expedientes, ref cantidad); break;
                    case 10: Operaciones.Resumen(expedientes, cantidad); break;
                    case 0: Console.WriteLine("Saliendo del sistema de Mesa de Partes Digital..."); break;
                    default: Console.WriteLine("Opción no válida. Elija un número entre 0 y 10."); break;
                }

                // Guardado automático: después de cada cambio no se pierde ningún trámite.
                if (huboCambios && clsArchivos.Guardar(expedientes, cantidad))
                    Console.WriteLine("(Guardado en " + clsArchivos.Ruta + ": " + cantidad + " expedientes)");
            } while (opc != 0);
        }

        // Lee el archivo al iniciar (también se usa en la opción 9 "Recargar").
        static void CargarDatos(ref Expediente[] exps, ref int cantidad)
        {
            int omitidas;
            if (!clsArchivos.Cargar(ref exps, ref cantidad, out omitidas))
            {
                Console.WriteLine("No existe " + clsArchivos.Ruta + ": se creará al registrar el primer expediente.");
                return;
            }

            Console.WriteLine("Expedientes cargados desde el archivo: " + cantidad);
            if (omitidas > 0)
                Console.WriteLine("ADVERTENCIA: se omitieron " + omitidas + " línea(s) inválida(s). Copia del original en "
                                  + clsArchivos.Ruta + ".bak");
        }

        static int MostrarMenu()
        {
            string[] opciones =
            {
                "1. Registrar expediente",
                "2. Buscar (código, apellido o asunto)",
                "3. Ordenar (código o apellido)",
                "4. Listar expedientes",
                "5. Modificar expediente",
                "6. Cambiar estado",
                "7. Eliminar expediente",
                "8. Guardar archivo ahora",
                "9. Recargar desde archivo",
                "10. Resumen por trámite y estado",
                "0. Salir"
            };

            Console.WriteLine();
            Console.WriteLine("=== MESA DE PARTES DIGITAL - " + NombreInstitucion + " ===");
            foreach (string linea in opciones)
                Console.WriteLine(linea);
            Console.WriteLine("Seleccione una opción:");
            return Entrada.LeerOpcion();
        }
    }
}
