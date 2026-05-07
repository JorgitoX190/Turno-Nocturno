using UnityEngine;

public class PilaRecargable : MonoBehaviour
{
    [Header("Configuración")]
    public float energiaQueAporta = 25f; // Cuánta batería recuperas al tomarla

    // Esta función de Unity se activa cuando alguien atraviesa el colisionador
    private void OnTriggerEnter(Collider otro)
    {
        // Verificamos si el objeto que nos tocó tiene el script de la linterna
        LinternaControl linternaDelJugador = otro.GetComponent<LinternaControl>();

        // Si efectivamente es el jugador (porque tiene el script)...
        if (linternaDelJugador != null)
        {
            // Le mandamos la energía
            linternaDelJugador.RecargarBateria(energiaQueAporta);
            
            // Destruimos la pila de la escena
            Destroy(gameObject);
        }
    }
}