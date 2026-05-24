using UnityEngine;

public class LimpiezaPorClickDirecto : MonoBehaviour
{
    [Header("Ajustes de Limpieza")]
    public int clicsNecesarios = 6;
    public float porcentajeReduccionPorClic = 0.2f; // Reducirá un 20% de su tamaño original por clic

    private int clicsActuales = 0;
    private Material materialCharco;
    private bool jugadorEnRango = false;
    private Camera camaraPrincipal;
    private Vector3 escalaOriginal;

    void Start()
    {
        camaraPrincipal = Camera.main;
        escalaOriginal = transform.localScale;

        // Intentar buscar el material en este objeto o en sus hijos
        Renderer renderer = GetComponentInChildren<Renderer>();
        if (renderer != null) materialCharco = renderer.material;
    }

    void Update()
    {
        if (jugadorEnRango && Input.GetMouseButtonDown(0))
        {
            DetectarClicEnCharco();
        }
    }

    void DetectarClicEnCharco()
    {
        if (camaraPrincipal == null) camaraPrincipal = Camera.main;

        Ray rayo = camaraPrincipal.ScreenPointToRay(Input.mousePosition);
        RaycastHit golpe;

        // Filtramos para que el rayo detecte colisiones físicas
        if (Physics.Raycast(rayo, out golpe))
        {
            // Verifica si tocamos este objeto o cualquiera de sus hijos colisionadores
            if (golpe.transform == this.transform || golpe.transform.IsChildOf(this.transform))
            {
                DarTrapeazo();
            }
        }
    }

    void DarTrapeazo()
    {
        clicsActuales++;

        // Calculamos la nueva escala reduciendo X y Z, pero manteniendo la altura original de Y intacta
        float factor = 1f - ((float)clicsActuales * porcentajeReduccionPorClic);
        factor = Mathf.Clamp01(factor);

        transform.localScale = new Vector3(escalaOriginal.x * factor, escalaOriginal.y, escalaOriginal.z * factor);

        // Desvanecer el material si tiene canal Alpha transparente
        if (materialCharco != null && materialCharco.HasProperty("_Color"))
        {
            Color c = materialCharco.color;
            c.a = 1f - ((float)clicsActuales / clicsNecesarios);
            materialCharco.color = c;
        }

        Debug.Log("¡Clic detectado en el desastre! Trapeadas: " + clicsActuales + "/" + clicsNecesarios);

        if (clicsActuales >= clicsNecesarios)
        {
            Debug.Log("¡Derrame eliminado de Walk-Mark!");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnRango = true;
            Debug.Log("Estás cerca del derrame. Haz clic directamente sobre él.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnRango = false;
        }
    }
}