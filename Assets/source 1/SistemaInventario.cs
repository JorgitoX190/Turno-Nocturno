using UnityEngine;
using UnityEngine.UI;

// Catálogo de objetos
public enum TipoItem { Vacio, Pastilla, Pila }

public class SistemaInventario : MonoBehaviour
{
    [Header("Memoria del Inventario")]
    public TipoItem[] slots = new TipoItem[3]; 

    [Header("Interfaz Visual")]
    public Image[] iconosSlots; 
    public Color colorVacio = new Color(1f, 1f, 1f, 0.5f); 
    
    [Header("Imágenes de Objetos")]
    public Sprite spritePastilla; 
    public Sprite spritePila;

    [Header("Conexión con otros Sistemas")]
    public LinternaControl miLinterna; // Esta es la línea que faltaba para la linterna

    [Header("Efectos")]
    public float curacionPorPastilla = 40f; 
    public float energiaPorPila = 50f;

    private SistemaCordura cordura; // Esta línea permite que el inventario hable con la cordura

    void Start()
    {
        for(int i = 0; i < slots.Length; i++) 
        {
            slots[i] = TipoItem.Vacio;
        }

        // Buscamos el script de cordura en el mismo objeto (Banana)
        cordura = GetComponent<SistemaCordura>();
        ActualizarUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) UsarObjeto(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) UsarObjeto(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) UsarObjeto(2);
    }

    public bool AgregarItem(TipoItem nuevoItem)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == TipoItem.Vacio)
            {
                slots[i] = nuevoItem;
                ActualizarUI();  
                return true;     
            }
        }
        return false; 
    }

    void UsarObjeto(int indice)
    {
        // 1. Usar PASTILLA
        if (slots[indice] == TipoItem.Pastilla) 
        {
            if (cordura != null) 
            {
                cordura.ModificarCordura(curacionPorPastilla);
            }
            slots[indice] = TipoItem.Vacio; 
            ActualizarUI(); 
        }
        // 2. Usar PILA
        else if (slots[indice] == TipoItem.Pila)
        {
            if (miLinterna != null) 
            {
                // Aquí activamos la función de recarga que ya tiene tu linterna
                miLinterna.RecargarBateria(energiaPorPila);
            }

            slots[indice] = TipoItem.Vacio; 
            ActualizarUI(); 
        }
    }

    void ActualizarUI()
    {
        for (int i = 0; i < iconosSlots.Length; i++)
        {
            if (slots[i] == TipoItem.Pastilla)
            {
                iconosSlots[i].sprite = spritePastilla;
                iconosSlots[i].color = Color.white; 
            }
            else if (slots[i] == TipoItem.Pila)
            {
                iconosSlots[i].sprite = spritePila;
                iconosSlots[i].color = Color.white; 
            }
            else
            {
                iconosSlots[i].sprite = null;
                iconosSlots[i].color = colorVacio;
            }
        }
    }
}