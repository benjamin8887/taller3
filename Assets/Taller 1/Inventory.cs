using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject[] inventorySlots; // Array de casillas del inventario
    public GameObject[] inventoryUse; // Array de casillas de la barra r�pida

    // private GameObject itemDragging; // Objeto que se est� arrastrando


    public void Start()
    {
        
    }
    /*
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && itemDragging == null)
        {
            // Intentar comenzar a arrastrar un objeto
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag("Item"))
            {
                itemDragging = hit.collider.gameObject;
            }
        }

        if (Input.GetMouseButton(0) && itemDragging != null)
        {
            // Arrastrar el objeto
            itemDragging.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0) && itemDragging != null)
        {
            // Soltar el objeto en una casilla
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag("InventorySlot"))
            {
                // Aqu� deber�as implementar la l�gica para apilar objetos si es del mismo tipo
                hit.collider.GetComponentInChildren<Image>().sprite = itemDragging.GetComponent<SpriteRenderer>().sprite;
                Destroy(itemDragging);
            }
            itemDragging = null;
        }
    }

    public void AddItem(GameObject item)
    {
        // Buscar una casilla vac�a en el inventario
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].GetComponentInChildren<Image>().sprite == null)
            {
                // La casilla est� vac�a, a�adir el objeto
                inventorySlots[i].GetComponentInChildren<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;
                Destroy(item); // Destruir el objeto en el mundo despu�s de ser recogido
                return;
            }
        }
        
        // Si no se encontr� una casilla vac�a, el inventario est� lleno
        Debug.Log("�El inventario est� lleno!");
    }
    */
}
