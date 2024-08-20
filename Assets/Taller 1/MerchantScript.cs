using UnityEngine;
using UnityEngine.UI;

public class MerchantScript : MonoBehaviour
{
    public GameObject shopInterface; // Referencia al panel de la tienda
    public Text itemNameText; // Referencia al texto para mostrar el nombre del objeto
    public Text itemPriceText; // Referencia al texto para mostrar el precio en bolsas de monedas
    public GameObject buyButton; // Referencia al botón de compra
    public Transform player; // Referencia al transform del jugador para determinar su posición
    public SpriteRenderer merchantSpriteRenderer; // Referencia al SpriteRenderer del mercader

    private bool isShopOpen = false;

    void Start()
    {
        CloseShop();
    }

    void Update()
    {
        // Cierra la tienda si el jugador presiona Esc (puedes cambiar esto según tus necesidades)
        if (Input.GetKeyDown(KeyCode.Escape) && isShopOpen)
        {
            CloseShop();
        }

        // Voltear el mercader para mirar al jugador
        if (player != null && merchantSpriteRenderer != null)
        {
            Vector3 directionToPlayer = player.position - transform.position;
            // Determina si el jugador está a la izquierda o derecha del mercader
            if (directionToPlayer.x > 0)
            {
                // El jugador está a la derecha del mercader
                merchantSpriteRenderer.flipX = false;
            }
            else
            {
                // El jugador está a la izquierda del mercader
                merchantSpriteRenderer.flipX = true;
            }
        }
    }

    public void OpenShop()
    {
        shopInterface.SetActive(true);
        isShopOpen = true;

        // Aquí podrías configurar qué productos mostrar en la tienda
        itemNameText.text = "Objeto a vender"; // Aquí debes poner el nombre del objeto que el mercader vende
        itemPriceText.text = "Precio: 100 Bolsas"; // Ejemplo de texto para el precio en bolsas de monedas

        // Configura la funcionalidad del botón de compra
        buyButton.GetComponent<Button>().onClick.AddListener(BuyItem); // Agrega un listener al botón de compra
    }

    public void CloseShop()
    {
        shopInterface.SetActive(false);
        isShopOpen = false;

        // Limpia cualquier listener que pueda haber en el botón de compra
        buyButton.GetComponent<Button>().onClick.RemoveAllListeners();
    }

    void BuyItem()
    {
        // Aquí implementa la lógica para comprar el objeto, verificar monedas, etc.
        Debug.Log("Comprando el objeto...");

        // Por ejemplo, puedes restar las monedas del jugador si tiene suficientes
        // Puedes acceder al inventario del jugador a través del script de PlayerMovement si es necesario
    }
}
