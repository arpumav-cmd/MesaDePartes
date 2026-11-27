# Mesa de Partes Digital – I.E. N.° 81770 «María Inmaculada Concepción» (Chao)

Prototipo de consola en C# para el **registro, validación, consulta, ordenamiento y almacenamiento permanente de expedientes**.
Proyecto de Fundamentos de Programación (CIIN1205P) – Evaluación Final (EF) – Universidad Privada del Norte.

## Integrantes (Grupo 4)

| Nombres | Código |
|---|---|
| Alaya Oruro, Daniel Alexander | N00534860 |
| Pardo Calderon, Ariana Carolina | N00549637 |
| Puma Vásquez, Andrea del Rosario | N00554902 |

Docente: Ing. Jorge Ricardo Perez Vigil

## Repositorio

(https://github.com/arpumav-cmd/MesadePartes_FundamentosP.git)

## Requisitos

- SDK de .NET 10
- Visual Studio Code con la extensión «C#» de Microsoft

## Instrucciones de ejecución

1. Clonar o descomprimir el proyecto.
2. Abrir la carpeta que contiene `MesaDePartes.csproj` en Visual Studio Code.
3. En la terminal de esa carpeta escribir: `dotnet run` (siempre desde esa carpeta: el archivo de datos se busca en `Data/`).
4. Elegir una opción del menú escribiendo su número y pulsando Enter. La opción 0 cierra el programa.

Para probar con datos ya cargados, copiar `Data/expedientes_ejemplo.txt` como `Data/expedientes.txt` antes de ejecutar.

## Funciones del menú

| Opción | Función |
|---|---|
| 1 | Registrar expediente (código único, DNI, nombres, trámite, asunto y fecha validados) |
| 2 | Buscar por código (lineal y, si la lista está ordenada, también binaria), por apellido o por texto del asunto |
| 3 | Ordenar por código o por apellido con el método burbuja |
| 4 | Listar todos los expedientes |
| 5 | Modificar un campo de un expediente |
| 6 | Cambiar el estado (Recibido, En trámite, Atendido) |
| 7 | Eliminar un expediente |
| 8 | Guardar el archivo ahora |
| 9 | Recargar los datos desde el archivo |
| 10 | Resumen por trámite y estado (tabla con arreglo bidimensional) |
| 0 | Salir |

## Persistencia (archivo como base de datos)

- Los datos se guardan en `Data/expedientes.txt`: una línea por expediente, 9 campos separados por `;`.
- Al iniciar, el programa **carga** el archivo automáticamente.
- Después de registrar, modificar, cambiar estado, eliminar u ordenar, el programa **guarda** automáticamente.
- Si el archivo no existe, se crea al registrar el primer expediente.
- Las líneas inválidas (campos faltantes, DNI o fecha incorrectos, código repetido) se omiten, se avisa en pantalla y se crea una copia `expedientes.txt.bak`.
- El carácter `;` no se permite dentro de los datos porque es el separador.

## Estructura del código (programación modular)

| Archivo | Responsabilidad |
|---|---|
| `Program.cs` | Menú principal, carga al iniciar y guardado automático |
| `Expediente.cs` | Entidad: los 9 datos de un trámite, `ToLinea()`, `ToString()` y `ToDetalle()` |
| `clsExpedientes.cs` | Lógica sin teclado ni pantalla: agregar (arreglo dinámico), búsquedas, burbuja, eliminar y resumen |
| `clsArchivos.cs` | Persistencia: `Guardar` y `Cargar` |
| `Operaciones.cs` | Flujo de cada opción del menú |
| `Entrada.cs` | Lectura por teclado con repetición hasta que el dato sea válido |
| `Validaciones.cs` | Validación de DNI, nombres, fecha y separador |

## Temas aplicados

- **Funciones y modularidad:** cada archivo tiene una sola responsabilidad y la lógica no depende de la consola.
- **Arreglos:** `Expediente[]` que crece con `Array.Resize`; `int[,]` en el resumen por trámite y estado.
- **Cadenas de caracteres:** `Split`, `Trim`, `StartsWith`, `Contains`, validación de DNI y nombres.
- **Recursividad:** `BuscarIndice`, `BuscarBinario`, `EstaOrdenadoPorCodigo`, `SoloDigitos` y `Listar`.
- **Archivos:** `StreamWriter`, `File.ReadLines` y manejo de errores con `try/catch`.

## Documentación

- `docs/diseno_logico.md`: pseudocódigo.
- `docs/casos_prueba.md` y `docs/datos_prueba.md`: pruebas con datos ficticios.
- `docs/archivo_corrupto_prueba.txt`: archivo con líneas inválidas para probar la carga.

Todos los datos de prueba son **ficticios**.
