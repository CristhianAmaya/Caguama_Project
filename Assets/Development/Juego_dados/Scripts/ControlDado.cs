using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlDado : MonoBehaviour
{
    private float ejeX, ejeY, ejeZ; // Variables para los ejes de rotación aleatoria del dado
    private Vector3 posicionInicial; // Posición inicial del dado
    private Rigidbody rbDado; // Componente Rigidbody del dado
    private bool dadoEnMovimiento = true; // Controla si el dado está en movimiento
    public ControlCara[] lados = new ControlCara[6]; // Array que almacena las 6 caras del dado
    private int valorDado; // Valor de la cara que está hacia arriba
    private int ladoOculto; // Valor de la cara que está contra el suelo

    void Start()
    {
        // Asigna las referencias de cada cara del dado al array `lados`
        for (int i = 0; i < lados.Length; i++)
        {
            lados[i] = gameObject.transform.GetChild(i + 6).GetComponent<ControlCara>();
        }

        // Configura la posición inicial y obtiene el Rigidbody del dado
        posicionInicial = transform.position;
        rbDado = GetComponent<Rigidbody>();

        // Prepara el dado para comenzar la tirada
        PrepararDado();
    }

    void Update()
    {
        // Detecta cuando se presiona la tecla 'Espacio' para preparar una nueva tirada
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PrepararDado();
        }

        // Detecta si el dado ha terminado de moverse
        if (rbDado.IsSleeping() && dadoEnMovimiento)
        {
            dadoEnMovimiento = false;
            // Obtiene el lado del dado que toca el suelo y calcula el valor de la cara superior
            ladoOculto = ComprobarLados();
            valorDado = lados.Length + 1 - ladoOculto;

            // Si el valor es inválido, aplica una fuerza para lanzar de nuevo el dado
            if (valorDado == lados.Length + 1)
            {
                rbDado.AddForce(3f, 0f, 0f, ForceMode.Impulse);
                dadoEnMovimiento = true;
            }
        }

        // Actualiza el valor del dado en el menú de control si el dado está quieto
        if (!dadoEnMovimiento)
        {
            // Se envía el nombre del dado y el valor de la cara superior del dado
            ControlMenu.instancia.ActualizarValor(gameObject.name, valorDado);
        }
    }

    // Método para resetear el dado y lanzarlo de nuevo con rotación y fuerza aleatoria
    void PrepararDado()
    {
        transform.position = posicionInicial;
        rbDado.velocity = Vector3.zero;
        ControlMenu.instancia.LimpiarValores();
        dadoEnMovimiento = true;

        // Aplica una rotación aleatoria
        ejeX = Random.Range(0f, 271f);
        ejeY = Random.Range(0f, 271f);
        ejeZ = Random.Range(0f, 271f);
        transform.Rotate(ejeX, ejeY, ejeZ);

        // Aplica una fuerza aleatoria para lanzar el dado
        ejeX = Random.Range(-3f, 3f);
        ejeY = Random.Range(-2f, 0f);
        ejeZ = Random.Range(-3f, 3f);
        rbDado.AddForce(ejeX, ejeY, ejeZ, ForceMode.Impulse);
    }

    // Método para determinar cuál cara del dado está tocando el suelo
    int ComprobarLados()
    {
        int valor = 0;
        for (int i = 0; i < lados.Length; i++)
        {
            // Comprueba si una cara está tocando el suelo
            if (lados[i].CompruebaSuelo())
            {
                valor = i + 1; // Ajusta el valor de la cara detectada
            }
        }
        return valor;
    }
}

