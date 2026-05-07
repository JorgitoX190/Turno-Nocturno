using UnityEngine;

public class ItemRecogible : MonoBehaviour
{
    [Header("¿Qué objeto es este?")]
    // Nos permite elegir en el Inspector qué es este objeto 3D
    public TipoItem tipoDeItem = TipoItem.Pastilla; 

    private void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag("Player"))
        {
            SistemaInventario inventario = otro.GetComponent<SistemaInventario>();

            if (inventario != null)
            {
                // Le pasamos el tipo de objeto que somos al inventario
                bool sePudoRecoger = inventario.AgregarItem(tipoDeItem);

                if (sePudoRecoger == true)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}