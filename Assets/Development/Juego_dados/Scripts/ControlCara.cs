using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCara : MonoBehaviour
{
    // Variable booleana para indicar si esta cara del dado está en contacto con el suelo
    private bool enElSuelo = false;

    // Método que se llama cuando un objeto con collider entra en contacto con el collider de esta cara del dado
    private void OnTriggerEnter(Collider other)
    {
        // Si el objeto con el que colisiona tiene la etiqueta "Rug" (representando el suelo)
        if (other.gameObject.tag == "Rug")
        {
            // Cambia el estado de 'enElSuelo' a true, indicando que esta cara está en contacto con el suelo
            enElSuelo = true;
        }
    }

    // Método que se llama cuando un objeto con collider sale del área de colisión de esta cara del dado
    private void OnTriggerExit(Collider other)
    {
        // Al dejar de estar en contacto con el suelo, cambia 'enElSuelo' a false
        enElSuelo = false;
    }

    // Método que permite verificar si esta cara del dado está en contacto con el suelo
    public bool CompruebaSuelo()
    {
        // Retorna el valor de 'enElSuelo' (true si está en el suelo, false si no)
        return enElSuelo;
    }
}

