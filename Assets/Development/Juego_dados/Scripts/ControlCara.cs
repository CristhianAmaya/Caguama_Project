using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCara : MonoBehaviour
{
    // Variable booleana para indicar si esta cara del dado est� en contacto con el suelo
    private bool enElSuelo = false;

    // M�todo que se llama cuando un objeto con collider entra en contacto con el collider de esta cara del dado
    private void OnTriggerEnter(Collider other)
    {
        // Si el objeto con el que colisiona tiene la etiqueta "Rug" (representando el suelo)
        if (other.gameObject.tag == "Rug")
        {
            // Cambia el estado de 'enElSuelo' a true, indicando que esta cara est� en contacto con el suelo
            enElSuelo = true;
        }
    }

    // M�todo que se llama cuando un objeto con collider sale del �rea de colisi�n de esta cara del dado
    private void OnTriggerExit(Collider other)
    {
        // Al dejar de estar en contacto con el suelo, cambia 'enElSuelo' a false
        enElSuelo = false;
    }

    // M�todo que permite verificar si esta cara del dado est� en contacto con el suelo
    public bool CompruebaSuelo()
    {
        // Retorna el valor de 'enElSuelo' (true si est� en el suelo, false si no)
        return enElSuelo;
    }
}

