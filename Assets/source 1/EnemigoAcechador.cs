using UnityEngine;
using UnityEngine.AI;

public class EnemigoAcechador : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public Transform jugador; 
    public float distanciaObservacion = 8f; 

    [Header("Configuración de Locura")]
    public float radioLocura = 10f; // Distancia a la que empieza a afectar la cordura
    public float danoPorSegundo = 5f; // Cuánta cordura te quita por segundo

    [Header("Animación")]
    public Animator animador; 

    private NavMeshAgent agente;
    private SistemaCordura corduraJugador; // Referencia al nuevo script de tu personaje

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        
        if(animador == null)
        {
            animador = GetComponent<Animator>();
        }

        // Al iniciar, el enemigo busca el script de cordura dentro de tu jugador
        if (jugador != null)
        {
            corduraJugador = jugador.GetComponent<SistemaCordura>();
        }
    }

    void Update()
    {
        if (jugador == null) return;

        float distanciaAlJugador = Vector3.Distance(transform.position, jugador.position);

        // --- LÓGICA DE MOVIMIENTO ---
        if (distanciaAlJugador > distanciaObservacion)
        {
            agente.isStopped = false;
            agente.SetDestination(jugador.position);
            if (animador != null) animador.SetBool("Caminando", true);
        }
        else
        {
            agente.isStopped = true;
            Vector3 direccion = (jugador.position - transform.position).normalized;
            direccion.y = 0; 
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direccion), Time.deltaTime * 5f);
            if (animador != null) animador.SetBool("Caminando", false);
        }

        // --- LÓGICA DE CORDURA (NUEVO) ---
        // Si el jugador entra en el aura de locura del enemigo...
        if (distanciaAlJugador <= radioLocura && corduraJugador != null)
        {
            // Le mandamos un número negativo para restarle a la barra progresivamente
            corduraJugador.ModificarCordura(-danoPorSegundo * Time.deltaTime);
        }
    }
}