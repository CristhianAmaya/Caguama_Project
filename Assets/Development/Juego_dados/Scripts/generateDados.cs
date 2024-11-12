using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateDados : MonoBehaviour
{
    public static generateDados instancia; // Instancia �nica de generateDados (Singleton)
    public GameObject dadoPrefab; // Prefab de los dados a instanciar
    public Vector3 initialPosition = new Vector3(); // Posici�n inicial del primer dado
    private List<Color> colorsDados = new List<Color>();
    [HideInInspector] public int cantidadDados = 4; // Cantidad total de dados a generar

    private void OnEnable()
    {
        if (instancia == null)
        {
            instancia = this;
        }
    }

    void Start()
    {
        // Array de posiciones para cada dado que ser� instanciado
        Vector3[] posicionesDado = new Vector3[cantidadDados];

        // Configura la posici�n inicial para el primer dado
        posicionesDado[0] = initialPosition;

        // Inicializa la lista de nombres de dados en `ControlMenu` para almacenar cada dado
        ControlMenu.instancia.namesDados = new List<string>(cantidadDados);

        // Llena la lista con un nombre temporal ("Inicialize") por cada dado
        for (int i = 0; i < cantidadDados; i++)
        {
            ControlMenu.instancia.namesDados.Add("Inicialize");
        }

        colorsDados = new List<Color>
        {
            Color.red, Color.green, Color.blue, Color.yellow
        };

        // Bucle para instanciar y posicionar cada dado
        for (int i = 0; i < cantidadDados; i++)
        {
            // Calcula la posici�n del siguiente dado si no es el primero
            if (i < cantidadDados - 1)
            {
                posicionesDado[i + 1] = new Vector3(
                    posicionesDado[i].x + 3.5f,
                    posicionesDado[i].y,
                    posicionesDado[i].z
                );
            }
            // Instancia el dado y lo posiciona en el espacio
            GameObject newDado = Instantiate(dadoPrefab, posicionesDado[i], Quaternion.identity);
            Renderer renderer_newDado = newDado.GetComponent<Renderer>();

            //Cambiamos el color del dado
            renderer_newDado.material.color = colorsDados[i];

            // Asigna un nombre �nico al dado basado en su n�mero
            newDado.name = $"Dado_{i + 1}";
            //Asigna a la lista los nombres de los dados generados
            ControlMenu.instancia.namesDados[i] = newDado.name;
        }
    }
}

