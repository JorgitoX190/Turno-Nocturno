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
    private SistemaCordura corduraJugador; 

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        
        if(animador == null)
        {
            animador = GetComponent<Animator>();
        }

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

        // --- LÓGICA DE DAÑO A LA CORDURA ---
        if (distanciaAlJugador <= radioLocura && corduraJugador != null)
        {
            // Calculamos un factor multiplicador: si el enemigo está rozando a Ángel, el daño es mayor
            float factorProximidad = 1f - (distanciaAlJugador / radioLocura);
            factorProximidad = Mathf.Clamp01(factorProximidad); 

            // Daño progresivo: a menor distancia, más pánico sufre Ángel
            float danoFinal = danoPorSegundo * (1f + factorProximidad);

            // Se envía el daño al script del jugador
            corduraJugador.ModificarCordura(-danoFinal * Time.deltaTime);
        }
    }
}