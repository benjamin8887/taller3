using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;


[System.Serializable]
public class itemAndSprite
{
    public string name;
    public Sprite sprite;
    public bool checkDistance;
    public GameObject otherObject;
    public GameObject firstObject;

    public UnityEvent onClick;
}

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryPanel; // Referencia al panel del inventario en Unity
    public GameObject inventorySlotPrefab; // Prefab del slot del inventario
    public GameObject inventoryItemPrefab; // Prefab del item del inventario
    public bool inventoryOpen = false; // Estado actual del inventario
    public Sprite defaultItemIcon; // Icono por defecto para los items


    [SerializeField] itemAndSprite[] itemSprites;
    [SerializeField] itemAndSprite[] permanentItemSprites;
    private GameObject[] items; // Array de items del inventario
    [SerializeField] Inventory inventory;
    [SerializeField] Transform itemPrefab;

    [SerializeField] Button[] QuickSlots; // Array de items del inventario

    void Start()
    {
        if (inventoryPanel == null)
        {
            Debug.LogError("El panel de inventario no está asignado en el Inspector.");
            return;
        }

        InitializeInventory();
    }

    public void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Debug.Log("1", this);
            if (QuickSlots[0] != null)
            {
                
                QuickSlots[0].onClick.Invoke();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (QuickSlots[1] != null)
            {
                QuickSlots[1].onClick.Invoke();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (QuickSlots[2] != null)
            {
                QuickSlots[2].onClick.Invoke();
            }
        }
        
    }

    void InitializeInventory()
    {
        // Obtener la cantidad correcta de slots del inventario
        int slotCount = inventoryPanel.transform.childCount;

        // Inicializar el array de items
        items = new GameObject[slotCount];

        // Inicializar los slots del inventario
        for (int i = 0; i < slotCount; i++)
        {
            // Asegurarse de que el índice i sea válido
            if (i < inventoryPanel.transform.childCount)
            {
                Transform slot = inventoryPanel.transform.GetChild(i);

                // Destruir cualquier item existente en el slot
                foreach (Transform child in slot)
                {
                    // Destroy(child.gameObject);
                }
            }
            else
            {
                Debug.LogError("El índice de inicialización del inventario está fuera de los límites.");
                continue; // Continuar con el siguiente ciclo si el índice está fuera de los límites
            }

            items[i] = null; // Inicializar todos los items a null (sin item)
        }
    }

    public void AddItemToInventory(string itemName, int amount)
    {
        // Lógica para agregar el objeto al inventario
        // Por ejemplo, puedes crear una instancia del objeto en el inventario visualmente o simplemente registrar que se ha recogido
        Debug.Log("Añadiendo " + amount + " de " + itemName + " al inventario.");

        // Aquí podrías realizar la lógica específica para añadir visualmente el objeto al inventario si es necesario
        Sprite sp = null;
        bool ispermanent = false;
        UnityEvent ua = null;
        
        bool checkDistance = false;
        GameObject otherDistance = null;
        GameObject firstObject = null;


        Debug.Log("Search ItemSprites ");
        for (int i = 0; i < itemSprites.Length; i++)
        {
            if (itemName == itemSprites[i].name)
            {
                sp = itemSprites[i].sprite;
                ua = itemSprites[i].onClick;
                checkDistance = itemSprites[i].checkDistance;
                otherDistance = itemSprites[i].otherObject;
                firstObject = itemSprites[i].firstObject;
            }
        }

        Debug.Log("Search PItemSprites ");
        for (int i = 0; i < permanentItemSprites.Length; i++)
        {
            if (itemName == itemSprites[i].name)
            {
                ispermanent = true;
                sp = permanentItemSprites[i].sprite;
                ua = permanentItemSprites[i].onClick;
                checkDistance = itemSprites[i].checkDistance;
                otherDistance = itemSprites[i].otherObject;
                firstObject = itemSprites[i].firstObject;
            }
        }

        Debug.Log("Create and assign");
        if (sp != null)
        {
            for (int i = 0; i < inventory.inventoryUse.Length; i++)
            {
                Debug.Log("IIU " + i);
                if (inventory.inventoryUse[i] == null)
                {
                    Debug.Log("Use" + inventory.inventorySlots[i]);
                    Transform item = Instantiate(itemPrefab, inventory.inventorySlots[i].transform.parent);
                    Image img = item.GetComponentInChildren<Image>();
                    Button btn = item.GetComponentInChildren<Button>();

                    GameObject goMe = this.gameObject;
                    GameObject goOther = otherDistance;
                    GameObject fromDistance = firstObject;
                    if (ispermanent == false) { 
                        btn.onClick.AddListener(() => {
                            
                            Debug.Log(fromDistance.transform.position + " " + goOther.transform.position);
                            Debug.Log(fromDistance.transform.position + " " + otherDistance.transform.position + " " + checkDistance);
                            if (checkDistance)
                            {
                                if(Vector2.Distance(fromDistance.transform.position, otherDistance.transform.position) < 10)
                                {
                                    ua.Invoke(); Destroy(item.gameObject);
                                }
                            }
                            else { 
                                ua.Invoke(); Destroy(item.gameObject);
                            }
                        });
                    }
                    else
                    {
                        btn.onClick.AddListener(() => { 
                            
                            ua.Invoke(); 
                            
                            });
                    }
                    item.position = inventory.inventorySlots[i].transform.position;
                    img.sprite = sp;
                    inventory.inventoryUse[i] = item.gameObject;
                    break;
                }
            }
            
        }
        else
        {
            Debug.LogWarning("Missing sprite for the name "+ itemName);
        }

    }


    public void ToggleInventory()
    {
        inventoryOpen = !inventoryOpen;
        inventoryPanel.SetActive(inventoryOpen);

        // Congelar juego si el inventario está abierto
        Time.timeScale = inventoryOpen ? 0 : 1;
    }
}
