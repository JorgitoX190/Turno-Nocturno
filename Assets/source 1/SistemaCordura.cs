using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering; // ¡NUEVO! Necesario para modificar los efectos de cámara

public class SistemaCordura : MonoBehaviour
{
    [Header("Configuración")]
    public float corduraMaxima = 100f;
    public float corduraActual;

    [Header("Interfaz")]
    public Slider barraCorduraUI;

    [Header("Efectos Visuales")]
    public Volume volumenLocura; // Aquí arrastraremos nuestro Global Volume

    void Start()
    {
        corduraActual = corduraMaxima;
        ActualizarBarra();
    }

    public void ModificarCordura(float cantidad)
    {
        corduraActual += cantidad;
        corduraActual = Mathf.Clamp(corduraActual, 0, corduraMaxima);
        ActualizarBarra();
    }

    void ActualizarBarra()
    {
        // 1. Actualizamos la barra visual
        if (barraCorduraUI != null)
        {
            barraCorduraUI.value = corduraActual;
        }

        // 2. Actualizamos la distorsión de la cámara
        if (volumenLocura != null)
        {
            // Calculamos qué tan "locos" estamos (un número de 0 a 1)
            // Si la cordura está al 100, la locura es 0. Si la cordura es 0, la locura es 1.
            float porcentajeLocura = 1f - (corduraActual / corduraMaxima);
            
            // Le aplicamos ese porcentaje al Peso (Weight) del filtro visual
            volumenLocura.weight = porcentajeLocura;
        }
    }
}