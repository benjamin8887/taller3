using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public GameObject item; // Objeto que se va a recoger (por ejemplo, una moneda)
    public int amount = 1;  // Cantidad de ese objeto

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
            if (inventoryManager != null)
            {
                inventoryManager.AddItemToInventory(item.name, amount); // A�adir el objeto al inventario
                // Destroy(gameObject); // Destruir el objeto en el mundo despu�s de ser recogido
                // this.enable 
                
            }
        }
    }
}
