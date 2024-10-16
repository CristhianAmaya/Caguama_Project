using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Vector3 positionPlayerInitial; // Posición donde se encuentra actualmente el personaje
    public Vector3 positionPlayerFinal; // Posición a la que se moverá el personaje
    public Camera mainCamera; // Cámara desde donde se disparará el Raycast
    public float speed = 5f;  // Velocidad a la que el personaje se moverá
    public float rotationSpeed = 10f; // Velocidad de rotación del personaje
    public Animator animator; // Para colcar las animaciones

    private bool isMoving = false; // Para verificar si el personaje está en movimiento

    // Start is called before the first frame update
    void Start()
    {
        // Asignamos la posición inicial del jugador en el escenario
        positionPlayerInitial = transform.position;
        positionPlayerFinal = transform.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Detectar si se ha hecho click con el botón izquierdo del mouse
        if (Input.GetMouseButtonDown(0))
        {
            // Se crea un rayo desde la cámara hasta el punto donde se hizo click
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Verificamos si el rayo ha colisionado con algún objeto etiquetado como "Floor"
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Floor"))
            {
                // Guardamos la posición del click en el plano (el suelo)
                positionPlayerFinal = hit.point;
                isMoving = true; // El personaje comienza a moverse
            }
        }

        // Si el personaje debe moverse
        if (isMoving)
        {
            // Calculamos la dirección del movimiento
            Vector3 movementDirection = (positionPlayerFinal - transform.position).normalized;

            // Rotar el personaje hacia la dirección de destino
            if (movementDirection != Vector3.zero) // Si los valores de movementDirection son diferentes a 0, se realiza la rotación
            {
                Quaternion targetRotation = Quaternion.LookRotation(movementDirection); // Se utiliza Quaternion.LookRotation para obtener la rotación hacia la dirección del click
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); // Utilizamos Quaternion.Slerp para interpolar suavemente la rotación del personaje
            }

            // Movemos el personaje hacia la posición final
            transform.position = Vector3.MoveTowards(transform.position, positionPlayerFinal, speed * Time.deltaTime);

            // Asignamos los valores a los parámetros del Animator
            animator.SetFloat("VelX", movementDirection.x); // Velocidad en el eje X
            animator.SetFloat("VelY", movementDirection.z); // Velocidad en el eje Z (usualmente en 3D)

            // Verificamos si ha llegado a la posición final
            if (Vector3.Distance(transform.position, positionPlayerFinal) < 0.1f)
            {
                // Actualizamos la posición inicial del jugador con la nueva posición
                positionPlayerInitial = positionPlayerFinal;

                isMoving = false; // El personaje ha llegado al destino

                // Detenemos el movimiento en el Animator
                animator.SetFloat("VelX", 0f);
                animator.SetFloat("VelY", 0f);
            }
        }
    }
}
