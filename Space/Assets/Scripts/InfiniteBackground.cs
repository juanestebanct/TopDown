using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    public float horizontalParallaxSpeed = 0.5f; // Velocidad de desplazamiento relativa horizontal del fondo
    public float verticalParallaxSpeed = 0.5f; // Velocidad de desplazamiento relativa vertical del fondo

    private Transform mainCameraTransform;
    private Vector3 previousCameraPosition;

    private void Start()
    {
        mainCameraTransform = Camera.main.transform;
        previousCameraPosition = mainCameraTransform.position;
    }

    private void Update()
    {
        // Calcular el desplazamiento de la cámara desde el frame anterior
        Vector3 cameraMovement = mainCameraTransform.position - previousCameraPosition;

        // Calcular el desplazamiento de fondo en función de la velocidad de paralaje
        Vector3 backgroundMovement = new Vector3(cameraMovement.x * horizontalParallaxSpeed, cameraMovement.y * verticalParallaxSpeed, 0f);

        // Aplicar el desplazamiento de fondo al objeto actual
        transform.position += backgroundMovement;

        // Actualizar la posición de la cámara para el siguiente frame
        previousCameraPosition = mainCameraTransform.position;
    }
}