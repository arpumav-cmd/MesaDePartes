# Datos ficticios para las pruebas

Todos los datos son inventados. No usar DNI ni nombres reales.

## Cuatro expedientes para probar el ordenamiento (ingresar en este orden)

| N.° | Código (lo ingresa el usuario) | DNI | Apellidos | Nombres | Grado | Trámite | Asunto | Fecha |
|---|---|---|---|---|---|---|---|---|
| 1 | EXP-001 | 10000001 | Quispe Rojas | Luis | 3ro A | Nueva matrícula | Solicita matrícula para el año 2026 | 02/03/2026 |
| 2 | EXP-002 | 10000002 | Torres Vega | Ana | 1ro B | Traslado | Solicita traslado desde otra institución | 05/03/2026 |
| 3 | EXP-003 | 10000003 | Zavaleta Cruz | Pedro | 5to C | Ratificación | Ratificación de matrícula | 09/03/2026 |
| 4 | EXP-004 | 10000004 | Alva Díaz | Rosa | 2do A | Nueva matrícula | Matrícula de estudiante nuevo | 12/03/2026 |

Orden esperado por apellido (opción 3): Alva (EXP-004), Quispe (EXP-001), Torres (EXP-002), Zavaleta (EXP-003).
Con estos datos el programa debe informar **6 comparaciones y 3 intercambios**.

## Seis expedientes más (para llenar el arreglo de 10 en el caso CP-07)

Usar los códigos EXP-005, EXP-006, EXP-007, EXP-008, EXP-009 y EXP-010 (en ese orden, uno por expediente).

| N.° | DNI | Apellidos | Nombres | Grado | Trámite | Asunto | Fecha |
|---|---|---|---|---|---|---|---|
| 5 | 10000005 | Prueba Cinco | Alumno Cinco | 1ro A | Nueva matrícula | Expediente de prueba cinco | 13/03/2026 |
| 6 | 10000006 | Prueba Seis | Alumno Seis | 1ro A | Nueva matrícula | Expediente de prueba seis | 13/03/2026 |
| 7 | 10000007 | Prueba Siete | Alumno Siete | 1ro A | Nueva matrícula | Expediente de prueba siete | 13/03/2026 |
| 8 | 10000008 | Prueba Ocho | Alumno Ocho | 1ro A | Nueva matrícula | Expediente de prueba ocho | 13/03/2026 |
| 9 | 10000009 | Prueba Nueve | Alumno Nueve | 1ro A | Nueva matrícula | Expediente de prueba nueve | 13/03/2026 |
| 10 | 10000010 | Prueba Diez | Alumno Diez | 1ro A | Nueva matrícula | Expediente de prueba diez | 13/03/2026 |

## Datos inválidos para probar las validaciones

| Campo | Valor inválido | Mensaje esperado |
|---|---|---|
| DNI | `1234567` (7 dígitos) | ERROR: El DNI debe tener exactamente 8 dígitos numéricos... |
| DNI | `12345A78` (con letra) | ERROR: El DNI debe tener exactamente 8 dígitos numéricos... |
| DNI | `00000000` | ERROR: El DNI debe tener exactamente 8 dígitos numéricos... |
| Apellidos | `R0jas` (con número) | ERROR: Use solo letras y espacios... |
| Asunto | `abc` (menos de 5 caracteres) | ERROR: Este campo debe tener al menos 5 caracteres. |
| Fecha | `31/02/2026` | ERROR: Esa fecha no existe en el calendario... |
| Fecha | `2026-03-10` | ERROR: Formato incorrecto. Use DD/MM/AAAA... |
| Fecha | `15/03/2019` | ERROR: Año no permitido. Debe estar entre 2020 y 2026. |
