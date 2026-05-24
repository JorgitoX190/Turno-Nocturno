using UnityEngine;
using TMPro;
using System.Collections;

public class ControladorNoches : MonoBehaviour
{
    [Header("Configuración de las Noches")]
    public int nocheActual = 1;
    public int tareasCompletadasEnNoche = 0;

    [Header("Interfaz de Narrativa (HUD)")]
    public TextMeshProUGUI textoIntercomunicado;
    public TextMeshProUGUI textoDialogoInterno;
    public TextMeshProUGUI textoContadorTareas;

    [Header("Zonas Bloqueadas (Niveles)")]
    public GameObject puertaCleaningRoom;
    public GameObject puertaJanitorRoom;
    public GameObject puertaFreezerRoom;
    public GameObject puertaBoilerRoom;
    public GameObject puertaGarageDoorRoom;

  void Start()
{
    // 1. Cerramos las zonas avanzadas al iniciar la Noche 1
    if (puertaFreezerRoom != null) puertaFreezerRoom.SetActive(true);
    if (puertaBoilerRoom != null) puertaBoilerRoom.SetActive(true);
    if (puertaGarageDoorRoom != null) puertaGarageDoorRoom.SetActive(true);

    // 2. Nos aseguramos de que las zonas iniciales SÍ estén abiertas
    if (puertaCleaningRoom != null) puertaCleaningRoom.SetActive(false);
    if (puertaJanitorRoom != null) puertaJanitorRoom.SetActive(false);

    // 3. Mensaje inicial en la pantalla de Ángel
    MostrarIntercomunicador("Bienvenido a tu turno, Ángel. Ve al Cleaning Room y organiza el desorden.");
    ActualizarHUDTareas();
}

    public void AvanzarTarea()
    {
        tareasCompletadasEnNoche++;
        ActualizarHUDTareas();
        VerificarProgresoNoche();
    }

    void VerificarProgresoNoche()
    {
        // Lógica de Narrativa por Tareas
        if (nocheActual == 1)
        {
            if (tareasCompletadasEnNoche == 1) {
                MostrarPensamiento("Siento que alguien me observa desde los pasillos...");
            }
            
            if (tareasCompletadasEnNoche == 3) {
                DesbloquearHabitacion(puertaFreezerRoom, "Freezer Room Abierto");
                MostrarIntercomunicador("Ángel, ve al Freezer Room. Hay un desorden con las carnes.");
            }
        }
    }

    // --- SISTEMA DE HUD NARRATIVO ---

    public void MostrarIntercomunicador(string mensaje) {
        StopAllCoroutines(); // Detiene mensajes anteriores
        StartCoroutine(EscribirTexto(textoIntercomunicado, "[GERENTE]: " + mensaje, 5f));
    }

    public void MostrarPensamiento(string mensaje) {
        StartCoroutine(EscribirTexto(textoDialogoInterno, "*" + mensaje + "*", 4f));
    }

    void ActualizarHUDTareas() {
        if (textoContadorTareas != null)
            textoContadorTareas.text = "Tareas completadas: " + tareasCompletadasEnNoche + "/6";
    }

    IEnumerator EscribirTexto(TextMeshProUGUI elemento, string contenido, float duracion) {
        elemento.text = contenido;
        elemento.canvasRenderer.SetAlpha(1.0f); // Se asegura que sea visible
        yield return new WaitForSeconds(duracion);
        elemento.CrossFadeAlpha(0, 1.5f, false); // Se desvanece suavemente en 1.5 segundos
    }

    void DesbloquearHabitacion(GameObject puerta, string log) {
        if (puerta != null) {
            puerta.SetActive(false);
            Debug.Log(log);
        }
    }
}