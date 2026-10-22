# Casos de prueba preliminares

La columna **Resultado obtenido** se llena al ejecutar el programa (con una captura de pantalla de cada caso).

| N.° | Caso | Entrada / pasos | Resultado esperado | Resultado obtenido |
|---|---|---|---|---|
| CP-01 | Registro válido de un expediente | Opción 1 con los datos del expediente 1 de `datos_prueba.md` | Mensaje «¡Éxito! Expediente [EXP-001] guardado correctamente.» | |
| CP-02 | Dato obligatorio inválido | Opción 1 con DNI `1234567`; luego asunto `abc` | El programa muestra el error y vuelve a pedir el dato; no avanza hasta que sea válido | |
| CP-03 | Código duplicado | Registrar EXP-001; luego, en un nuevo registro, ingresar otra vez `EXP-001` | «ERROR: El código EXP-001 ya existe. Ingrese otro.» y vuelve a pedir el código | |
| CP-04 | Búsqueda de expediente existente | Opción 2, por código, `EXP-001` | Se muestran los datos completos del expediente | |
| CP-05 | Búsqueda de expediente inexistente | Opción 2, por código, `EXP-999` | «No se encontró ningún expediente con el código: EXP-999» | |
| CP-06 | Ordenamiento de varios expedientes en desorden | Registrar los 4 expedientes de `datos_prueba.md` y elegir la opción 3 | Lista ordenada: Alva, Quispe, Torres, Zavaleta; 6 comparaciones y 3 intercambios | |
| CP-07 | Capacidad completa (arreglo de tamaño fijo) | Registrar 10 expedientes e intentar el número 11 | «Error: Memoria llena, no se pueden registrar más expedientes.» | |
