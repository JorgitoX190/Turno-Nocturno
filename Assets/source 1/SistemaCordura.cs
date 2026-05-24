using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering; // �NUEVO! Necesario para modificar los efectos de c�mara

public class SistemaCordura : MonoBehaviour
{
    [Header("Configuraci�n")]
    public float corduraMaxima = 100f;
    public float corduraActual;

    [Header("Interfaz")]
    public Slider barraCorduraUI;

    [Header("Efectos Visuales")]
    public Volume volumenLocura; // Aqu� arrastraremos nuestro Global Volume

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

        // 2. Actualizamos la distorsi�n de la c�mara
        if (volumenLocura != null)
        {
            // Calculamos qu� tan "locos" estamos (un n�mero de 0 a 1)
            // Si la cordura est� al 100, la locura es 0. Si la cordura es 0, la locura es 1.
            float porcentajeLocura = 1f - (corduraActual / corduraMaxima);
            
            // Le aplicamos ese porcentaje al Peso (Weight) del filtro visual
            volumenLocura.weight = porcentajeLocura;
        }
    }
}