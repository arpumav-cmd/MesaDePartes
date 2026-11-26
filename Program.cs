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
 
                switch (opc)
                {
                    case 1:
                        huboCambios = Operaciones.RegistrarExpediente(ref expedientes, ref cantidad);
                        break;
                    case 2:
                        Operaciones.BuscarExpediente(expedientes, cantidad);
                        break;
                    case 3:
                        huboCambios = Operaciones.OrdenarExpedientes(expedientes, cantidad);
                        break;
                    case 4:
                        Operaciones.ListarExpedientes(expedientes, cantidad);
                        break;
                    case 5:
                        huboCambios = Operaciones.ModificarExpediente(expedientes, cantidad);
                        break;
                    case 6:
                        huboCambios = Operaciones.CambiarEstado(expedientes, cantidad);
                        break;
                    case 7:
                        huboCambios = Operaciones.EliminarExpediente(expedientes, ref cantidad);
                        break;
                    case 8:
                        GuardarManual(expedientes, cantidad);
                        break;
                    case 9:
                        CargarDatos(ref expedientes, ref cantidad);
                        break;
                    case 0:
                        Console.WriteLine("Saliendo del sistema de Mesa de Partes Digital...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Por favor, elija un número entre 0 y 9.");
                        break;
                }
 
                if (huboCambios)
                    GuardarAutomatico(expedientes, cantidad);
            } while (opc != 0);
        }
 
        // Lee el archivo al iniciar (y también en la opción 9 "Recargar").
        static void CargarDatos(ref Expediente[] exps, ref int cantidad)
        {
            int omitidas;
            bool existe = clsArchivos.Cargar(clsArchivos.Ruta, ref exps, ref cantidad, out omitidas);
 
            if (!existe)
            {
                Console.WriteLine("No existe el archivo " + clsArchivos.Ruta + ". Se creará al registrar el primer expediente.");
                return;
            }
 
            Console.WriteLine("Expedientes cargados desde el archivo: " + cantidad);
            if (omitidas > 0)
            {
                Console.WriteLine("ADVERTENCIA: se omitieron " + omitidas + " línea(s) inválida(s) del archivo.");
                clsArchivos.Respaldar(clsArchivos.Ruta);
                Console.WriteLine("Se guardó una copia del archivo original en " + clsArchivos.Ruta + ".bak");
            }
        }
 
        // Guarda tras cada cambio para no perder ningún trámite.
        static void GuardarAutomatico(Expediente[] exps, int cantidad)
        {
            if (clsArchivos.Guardar(clsArchivos.Ruta, exps, cantidad))
                Console.WriteLine("(Cambios guardados en " + clsArchivos.Ruta + ")");
            else
                Console.WriteLine("ADVERTENCIA: los cambios siguen en memoria pero NO se pudieron guardar.");
        }
 
        // Opción 8 del menú.
        static void GuardarManual(Expediente[] exps, int cantidad)
        {
            if (clsArchivos.Guardar(clsArchivos.Ruta, exps, cantidad))
                Console.WriteLine("Archivo guardado: " + clsArchivos.Ruta + " (" + cantidad + " expedientes).");
        }
 
        // ---------- MENÚ ----------
        static int MostrarMenu()
        {
            Console.WriteLine();
            Console.WriteLine("==================================================");
            Console.WriteLine(" MESA DE PARTES DIGITAL - " + NombreInstitucion);
            Console.WriteLine("==================================================");
            Console.WriteLine("1. Registrar Expediente");
            Console.WriteLine("2. Buscar Expediente (código, apellido o asunto)");
            Console.WriteLine("3. Ordenar (por código o apellido)");
            Console.WriteLine("4. Listar Todos los Expedientes");
            Console.WriteLine("5. Modificar Expediente");
            Console.WriteLine("6. Cambiar Estado del Expediente");
            Console.WriteLine("7. Eliminar Expediente");
            Console.WriteLine("8. Guardar archivo ahora");
            Console.WriteLine("9. Recargar desde archivo");
            Console.WriteLine("0. Salir");
            Console.WriteLine("Seleccione una opción (0-9):");
            return Entrada.LeerOpcion();
        }
    }
}
