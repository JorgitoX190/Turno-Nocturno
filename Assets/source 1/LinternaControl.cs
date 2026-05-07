using UnityEngine;

public class LinternaControl : MonoBehaviour
{
    [Header("Componentes de Luz")]
    public Light luzLinterna;       
    public GameObject hazVisual;  

    [Header("Configuración de Teclas")]
    public KeyCode teclaApuntar = KeyCode.F;

    [Header("Sistema de Batería")]
    public float bateriaMaxima = 100f;
    public float bateriaActual;
    public float velocidadDescarga = 5f; 
    
    private bool linternaEncendida = false;

    void Start()
    {
        // Iniciamos con la batería al 100% y la linterna apagada
        bateriaActual = bateriaMaxima;
        SetLinterna(false); 
    }

    void Update()
    {
        // SISTEMA DE INTERRUPTOR (Toggle)
        if (Input.GetKeyDown(teclaApuntar))
        {
            if (linternaEncendida)
            {
                // Si estaba encendida, la apagamos
                SetLinterna(false);
            }
            else if (bateriaActual > 0)
            {
                // Si estaba apagada y tenemos pila, la encendemos
                SetLinterna(true);
            }
        }

        // Lógica de descarga por tiempo
        if (linternaEncendida)
        {
            bateriaActual -= velocidadDescarga * Time.deltaTime;

            // Si la batería se acaba, apagamos la linterna a la fuerza
            if (bateriaActual <= 0)
            {
                bateriaActual = 0;
                SetLinterna(false);
            }
        }
    }

    void SetLinterna(bool estado)
    {
        linternaEncendida = estado;
        
        if(luzLinterna != null) luzLinterna.enabled = estado;
        if(hazVisual != null) hazVisual.SetActive(estado);
    }

    // Esta función la llama el SistemaInventario
    public void RecargarBateria(float cantidad)
    {
        bateriaActual += cantidad;
        
        // Evitamos que la batería pase del límite máximo
        if (bateriaActual > bateriaMaxima)
        {
            bateriaActual = bateriaMaxima;
        }
    }
}