using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ControlMenu : MonoBehaviour
{
    public static ControlMenu instancia; // Instancia única de ControlMenu (Singleton)
    public List<int> valoresDados; // Lista para almacenar los valores de cada dado
    public List<string> namesDados; // Lista para almacenar los nombres de cada dado
    public TextMeshProUGUI textos; // Elemento de UI para mostrar los valores

    void Start()
    {
        valoresDados = new List<int>(generateDados.instancia.cantidadDados);

        // Inicializa la lista de valores con ceros para cada dado. Esto se hace para que la lista no genere ningún error
        for (int i = 0; i < generateDados.instancia.cantidadDados; i++)
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
        for (int i = 0; i < generateDados.instancia.cantidadDados; i++)
        {
            if (namesDados[i] == nameDadito)
            {
                if (valoresDados[i] == 0 && dado != 0)
                {
                    // Incrementa dadosQuietos solo si el valor cambia de cero a un valor distinto de cero
                    mechanicsDados.instancia.dadosQuietos++;
                }
                valoresDados[i] = dado;
            }
        }
    }

    // Resetea los valores de todos los dados a cero
    public void LimpiarValores()
    {
        for (int i = 0; i < valoresDados.Count; i++)
        {
            valoresDados[i] = 0;
        }
    }

    // Imprime los valores de cada dado en el UI
    public void imprimir(Dictionary<int, int> valoresIconos)
    {
        // Limpiar el texto antes de agregar nuevo contenido
        textos.text = "";

        // Agregar información sobre los valores de los dados
        textos.text +=
            $"Valor dado 1 = {valoresDados[0]}\n" +
            $"Valor dado 2 = {valoresDados[1]}\n" +
            $"Valor dado 3 = {valoresDados[2]}\n" +
            $"Valor dado 4 = {valoresDados[3]}\n";

        // Recorrer cada par clave-valor en el diccionario valoresIconos
        foreach (KeyValuePair<int, int> par in valoresIconos)
        {
            // par.Key representa el icono, y par.Value representa la cantidad de veces que aparece
            textos.text += $"Icono {par.Key} aparece {par.Value} veces\n";
        }
    }
}
