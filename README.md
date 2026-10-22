# Mesa de Partes Digital – I.E. N.° 81770 «María Inmaculada Concepción» (Chao)

Prototipo de consola en C# para el **registro, consulta y ordenamiento de expedientes de matrícula**.
Proyecto de Fundamentos de Programación (CIIN1205) – Examen Parcial (EP) – Universidad Privada del Norte.

## Integrantes (Grupo 4)

| Nombres | Código |
|---|---|
| Alaya Oruro, Daniel Alexander | N00534860 |
| Pardo Calderon, Ariana Carolina | N00549637 |
| Puma Vásquez, Andrea del Rosario | N00554902 |

Docente: Ing. Jorge Ricardo Perez Vigil

## Repositorio

[PEGAR AQUÍ EL ENLACE DE GITHUB]

## Requisitos

- SDK de .NET 10
- Visual Studio Code con la extensión «C#» de Microsoft

## Instrucciones de ejecución

1. Clonar o descomprimir el proyecto.
2. Abrir la carpeta que contiene `MesaDePartes.csproj` en Visual Studio Code.
3. En la terminal de la carpeta escribir: `dotnet run`
4. Elegir una opción del menú escribiendo su número y pulsando Enter. La opción 7 cierra el programa.

## Funciones del menú

| Opción | Función |
|---|---|
| 1 | Registrar expediente (ingreso de código único sin repetir, validación de DNI, nombres, trámite, asunto y fecha) |
| 2 | Buscar expediente por código o por apellido (búsqueda lineal) |
| 3 | Ordenar por apellido con el método burbuja (muestra las pasadas) |
| 4 | Listar todos los expedientes |
| 5 | Modificar un expediente |
| 6 | Eliminar un expediente |
| 7 | Salir |

## Estructura del código

| Archivo | Contenido |
|---|---|
| `Program.cs` | Menú principal |
| `Datos.cs` | Arreglos paralelos, total de expedientes y búsqueda por código |
| `Validaciones.cs` | Validación de DNI, nombres y fecha |
| `Entrada.cs` | Lectura de datos con repetición hasta que sean válidos |
| `Operaciones.cs` | Registrar, buscar, ordenar, listar, modificar y eliminar |

## Alcance de esta etapa (EP)

- Los expedientes se guardan **solo en memoria** (arreglos paralelos de tamaño fijo: 10).
- No se usan archivos, base de datos ni interfaz gráfica; la persistencia se incorporará en la EF.
- Los datos de prueba son **ficticios** (ver `docs/datos_prueba.md`).
- Documentos: `docs/casos_prueba.md` y `docs/diseno_logico.md`.
# MesaDePartes
