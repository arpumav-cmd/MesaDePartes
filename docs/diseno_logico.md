# Diseño lógico (pseudocódigo)

Pseudocódigo de las tres operaciones principales. El PSeInt completo del grupo está en esta misma carpeta.

## Registro

```
SubProceso RegistrarExpediente
    Si total >= Capacidad Entonces
        Escribir "Error: Memoria llena"
    Sino
        PedirCodigo(cod)           // repite hasta que el código no esté repetido
        PedirDNI(dni)              // repite hasta que sea válido
        PedirNombre(ape), PedirNombre(nom)
        PedirTexto(grado), PedirTexto(tipo), PedirTexto(asunto)
        PedirFecha(fec)            // repite hasta que sea válida
        guardar los datos en la posición "total" de cada arreglo
        estado[total] <- "Recibido"
        total <- total + 1
        Escribir "Expediente guardado"
    FinSi
FinSubProceso
```

## Búsqueda lineal por código

```
SubProceso pos <- BuscarIndice(buscado)
    pos <- -1
    k <- 0
    Mientras k < total Y pos = -1 Hacer
        Si codigos[k] = buscado Entonces
            pos <- k
        FinSi
        k <- k + 1
    FinMientras
FinSubProceso
```

## Ordenamiento burbuja por apellido

```
SubProceso OrdenarExpedientes
    Para i <- 0 Hasta total - 2 Hacer
        Para j <- 0 Hasta total - i - 2 Hacer
            Si ClaveOrden(j) > ClaveOrden(j + 1) Entonces
                intercambiar la posición j con la j + 1 en TODOS los arreglos
            FinSi
        FinPara
    FinPara
FinSubProceso
```
