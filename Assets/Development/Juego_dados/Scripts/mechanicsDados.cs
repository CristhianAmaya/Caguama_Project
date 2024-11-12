using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class mechanicsDados : MonoBehaviour
{
    public static mechanicsDados instancia; // Instancia para acceder al script desde otras clases
    private List<string> succesGrades; // Lista de grados de éxito: Regular, Superior, Avanzado, Imposible
    public int dadosQuietos; // Contador de dados que se han detenido
    public TextMeshProUGUI texto_succesGrades; // Texto de UI para mostrar el grado de éxito
    [HideInInspector] public string gradoExito;

    void Start()
    {
        // Inicializamos la lista de grados de éxito en orden
        succesGrades = new List<string>() { "Regular", "Superior", "Avanzado", "Imposible" };
        dadosQuietos = 0; // Inicialmente no hay dados detenidos

        // Configuramos la instancia si no está ya configurada (patrón singleton)
        if (instancia == null)
        {
            instancia = this;
        }
    }

    void Update()
    {
        // Comprobamos si todos los dados se han detenido
        if (dadosQuietos == generateDados.instancia.cantidadDados)
        {
            // Ejecutamos la rutina de comparación cuando todos los dados están quietos
            StartCoroutine(comparision());
            dadosQuietos = 0; // Reiniciamos el contador para la próxima tirada
        }
    }

    IEnumerator comparision()
    {
        // Esperamos un momento antes de iniciar la comparación (simula una pausa de 2.5 segundos)
        yield return new WaitForSeconds(1f);
        Debug.Log("Todos los dados se han detenido. Ejecutando lógica de comparación...");

        // Diccionario que asocia cada cara del dado con el icono que representa
        Dictionary<int, int> iconMap = new Dictionary<int, int>
        {
            { 1, 0 }, { 12, 0 }, // La cara 1 y la cara 12 tienen el mismo icono (índice 0)
            { 2, 1 }, { 11, 1 }, // La cara 2 y la cara 11 tienen el mismo icono (índice 1)
            { 3, 2 }, { 10, 2 }, // La cara 3 y la cara 10 tienen el mismo icono (índice 2)
            { 4, 3 }, { 9, 3 },  // La cara 4 y la cara 9 tienen el mismo icono (índice 3)
            { 5, 4 }, { 8, 4 },  // La cara 5 y la cara 8 tienen el mismo icono (índice 4)
            { 6, 5 }, { 7, 5 }   // La cara 6 y la cara 7 tienen el mismo icono (índice 5)
        };

        // Diccionario que cuenta cuántas veces aparece cada icono en la tirada de los dados
        Dictionary<int, int> iconCount = new Dictionary<int, int>();

        // Obtenemos los valores actuales de los dados desde otro script (ControlMenu)
        List<int> valoresDados = ControlMenu.instancia.valoresDados;

        // Para cada valor en la lista de valores de los dados, hacemos el conteo de iconos
        foreach (int valor in valoresDados)
        {
            int icon = iconMap[valor]; // Usamos el diccionario para obtener el icono correspondiente al valor del dado

            // Si el icono ya existe en el conteo, aumentamos su cuenta en 1
            if (iconCount.ContainsKey(icon))
            {
                iconCount[icon]++;
            }
            else // Si el icono no está en el conteo, lo agregamos con un conteo inicial de 1
            {
                iconCount[icon] = 1;
            }
        }

        // Inicializamos una variable para el grado de éxito con un valor por defecto
        gradoExito = "Sin éxito";

        // Lógica para determinar el grado de éxito basado en el conteo de iconos
        if (iconCount.ContainsValue(4))
        {
            // Si algún icono aparece 4 veces, es un grado "Imposible"
            gradoExito = succesGrades[3]; // Usamos el índice 3 de succesGrades para obtener "Imposible"
        }
        else if (iconCount.ContainsValue(3))
        {
            // Si algún icono aparece 3 veces, es un grado "Superior"
            gradoExito = succesGrades[1]; // Usamos el índice 1 de succesGrades para obtener "Superior"
        }
        else if (iconCount.ContainsValue(2))
        {
            // Para el grado "Avanzado" necesitamos comprobar si hay dos pares de iconos iguales
            int pairs = 0; // Contador de pares

            // Recorremos los valores en el diccionario para contar los pares
            foreach (var count in iconCount.Values)
            {
                if (count == 2) // Si el conteo de un icono es 2, contamos un par
                {
                    pairs++;
                }
            }

            if (pairs == 2) // Si encontramos dos pares, es un grado "Avanzado"
            {
                gradoExito = succesGrades[2]; // Usamos el índice 2 de succesGrades para obtener "Avanzado"
            }
            else
            {
                // Si solo hay un par, el grado es "Regular"
                gradoExito = succesGrades[0]; // Usamos el índice 0 de succesGrades para obtener "Regular"
            }
        }

        ControlMenu.instancia.imprimir(iconCount);
        // Actualizamos el texto en la interfaz con el grado de éxito final
        texto_succesGrades.text = $"Grado de éxito: {gradoExito}";
        Debug.Log($"Grado de éxito: {gradoExito}"); // Mensaje de depuración para confirmar el grado de éxito en la consola
    }
}
