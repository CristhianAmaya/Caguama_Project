using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Vector3 positionPlayerInitial; // Posici�n donde se encuentra actualmente el personaje
    public Vector3 positionPlayerFinal; // Posici�n a la que se mover� el personaje
    public Camera mainCamera; // C�mara desde donde se disparar� el Raycast
    public float speed = 5f;  // Velocidad a la que el personaje se mover�
    public float rotationSpeed = 10f; // Velocidad de rotaci�n del personaje
    public Animator animator; // Para colcar las animaciones

    private bool isMoving = false; // Para verificar si el personaje est� en movimiento

    // Start is called before the first frame update
    void Start()
    {
        // Asignamos la posici�n inicial del jugador en el escenario
        positionPlayerInitial = transform.position;
        positionPlayerFinal = transform.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Detectar si se ha hecho click con el bot�n izquierdo del mouse
        if (Input.GetMouseButtonDown(0))
        {
            // Se crea un rayo desde la c�mara hasta el punto donde se hizo click
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Verificamos si el rayo ha colisionado con alg�n objeto etiquetado como "Floor"
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Floor"))
            {
                // Guardamos la posici�n del click en el plano (el suelo)
                positionPlayerFinal = hit.point;
                isMoving = true; // El personaje comienza a moverse
            }
        }

        // Si el personaje debe moverse
        if (isMoving)
        {
            // Calculamos la direcci�n del movimiento
            Vector3 movementDirection = (positionPlayerFinal - transform.position).normalized;

            // Rotar el personaje hacia la direcci�n de destino
            if (movementDirection != Vector3.zero) // Si los valores de movementDirection son diferentes a 0, se realiza la rotaci�n
            {
                Quaternion targetRotation = Quaternion.LookRotation(movementDirection); // Se utiliza Quaternion.LookRotation para obtener la rotaci�n hacia la direcci�n del click
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); // Utilizamos Quaternion.Slerp para interpolar suavemente la rotaci�n del personaje
            }

            // Movemos el personaje hacia la posici�n final
            transform.position = Vector3.MoveTowards(transform.position, positionPlayerFinal, speed * Time.deltaTime);

            // Asignamos los valores a los par�metros del Animator
            animator.SetFloat("VelX", movementDirection.x); // Velocidad en el eje X
            animator.SetFloat("VelY", movementDirection.z); // Velocidad en el eje Z (usualmente en 3D)

            // Verificamos si ha llegado a la posici�n final
            if (Vector3.Distance(transform.position, positionPlayerFinal) < 0.1f)
            {
                // Actualizamos la posici�n inicial del jugador con la nueva posici�n
                positionPlayerInitial = positionPlayerFinal;

                isMoving = false; // El personaje ha llegado al destino

                // Detenemos el movimiento en el Animator
                animator.SetFloat("VelX", 0f);
                animator.SetFloat("VelY", 0f);
            }
        }
    }
}
