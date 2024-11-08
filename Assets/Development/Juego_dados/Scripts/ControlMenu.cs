using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ControlMenu : MonoBehaviour
{
    public static ControlMenu instancia; // Instancia única de ControlMenu (Singleton)
    [HideInInspector] public generateDados generateDados_instance; // Referencia a la instancia de generación de dados
    public List<int> valoresDados; // Lista para almacenar los valores de cada dado
    public List<string> namesDados; // Lista para almacenar los nombres de cada dado
    public TextMeshProUGUI textos; // Elemento de UI para mostrar los valores

    void Start()
    {
        // Se utiliza FindObjectOfType para buscar un objeto que tenga el Script "generateDados"
        generateDados_instance = FindObjectOfType<generateDados>();
        valoresDados = new List<int>(generateDados_instance.cantidadDados);

        // Inicializa la lista de valores con ceros para cada dado. Esto se hace para que la lista no genere ningún error
        for (int i = 0; i < generateDados_instance.cantidadDados; i++)
        {
            valoresDados.Add(0);
        }
    }

    private void OnEnable()
    {
        if (instancia == null)
        {
            instancia = this;
        }
    }

    // Actualiza el valor de un dado específico basado en su nombre
    public void ActualizarValor(string nameDadito, int dado)
    {
        for (int i = 0; i < generateDados_instance.cantidadDados; i++)
        {
            if (namesDados[i] == nameDadito) // Busca el dado por su nombre
            {
                valoresDados[i] = dado; // Asigna el valor al índice correcto
            }
        }
        imprimir();
    }

    // Resetea los valores de todos los dados a cero
    public void LimpiarValores()
    {
        for (int i = 0; i < valoresDados.Count; i++)
        {
            valoresDados[i] = 0;
        }
        imprimir();
    }

    // Imprime los valores de cada dado en el UI
    private void imprimir()
    {
        textos.text =
            $"Valor dado 1 = {valoresDados[0]}\n" +
            $"Valor dado 2 = {valoresDados[1]}\n" +
            $"Valor dado 3 = {valoresDados[2]}\n" +
            $"Valor dado 4 = {valoresDados[3]}";
    }
}
