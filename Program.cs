using System.Text;

namespace MesaDePartes
{
    public class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = new UTF8Encoding(false);   // para ver tildes y ñ
            Console.InputEncoding = new UTF8Encoding(false);

            int opc;
            do
            {
                opc = MostrarMenu();

                switch (opc)
                {
                    case 1: Operaciones.RegistrarExpediente(); break;
                    case 2: Operaciones.BuscarExpediente(); break;
                    case 3: Operaciones.OrdenarExpedientes(); break;
                    case 4: Operaciones.ListarExpedientes(); break;
                    case 5: Operaciones.ModificarExpediente(); break;
                    case 6: Operaciones.EliminarExpediente(); break;
                    case 7: Console.WriteLine("Saliendo del sistema de Mesa de Partes Digital..."); break;
                    default: Console.WriteLine("Opción no válida. Por favor, elija un número entre 1 y 7."); break;
                }
            } while (opc != 7);
        }

        // ---------- MENÚ ----------
        static int MostrarMenu()
        {
            Console.WriteLine();
            Console.WriteLine("==================================================");
            Console.WriteLine(" MESA DE PARTES DIGITAL - I.E. N.° 81770");
            Console.WriteLine("==================================================");
            Console.WriteLine("1. Registrar y Almacenar Expediente");
            Console.WriteLine("2. Buscar Expediente (por código o apellido)");
            Console.WriteLine("3. Ordenar por Apellido A-Z");
            Console.WriteLine("4. Listar Todos los Expedientes");
            Console.WriteLine("5. Modificar Expediente");
            Console.WriteLine("6. Eliminar Expediente");
            Console.WriteLine("7. Salir");
            Console.WriteLine("Seleccione una opción (1-7):");
            return Entrada.LeerOpcion();
        }
    }
}
