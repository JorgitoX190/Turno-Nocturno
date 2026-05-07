using UnityEngine;

public class ControlLinternaTerceraPersona : MonoBehaviour
{
    [Header("Referencias")]
    public Transform cameraTransform; // La Main Camera para saber hacia dónde apuntar
    public Transform originTransform; // El objeto 'OrigenLinterna' frente a la cara

    [Header("Ajuste Fino")]
    public float distanciaHaciaAdelante = 0.1f; // Pequeño empujón hacia adelante para que no ilumine la cara por dentro

    // Usamos LateUpdate para asegurarnos de que la cámara y el personaje ya se movieron
    void LateUpdate()
    {
        if (cameraTransform == null || originTransform == null) return;

        // 1. Copiar la posición exacta del origen frente a la cara
        transform.position = originTransform.position;

        // 2. Empujar la luz un poquito más hacia adelante siguiendo la vista
        // Esto previene que la luz ilumine la propia cara del personaje
        transform.position += cameraTransform.forward * distanciaHaciaAdelante;

        // 3. Copiar la rotación de la cámara (hacia dónde miramos)
        transform.rotation = cameraTransform.rotation;
    }
}