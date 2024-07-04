using UnityEngine;
using UnityEngine.UI;

public class MerchantScript : MonoBehaviour
{
    public GameObject shopInterface; // Referencia al panel de la tienda
    public Text itemNameText; // Referencia al texto para mostrar el nombre del objeto
    public Text itemPriceText; // Referencia al texto para mostrar el precio en bolsas de monedas
    public GameObject buyButton; // Referencia al bot�n de compra

    private bool isShopOpen = false;

    void Start()
    {
        CloseShop();
    }

    void Update()
    {
        // Cierra la tienda si el jugador presiona Esc (puedes cambiar esto seg�n tus necesidades)
        if (Input.GetKeyDown(KeyCode.Escape) && isShopOpen)
        {
            CloseShop();
        }
    }

    public void OpenShop()
    {
        shopInterface.SetActive(true);
        isShopOpen = true;

        // Aqu� podr�as configurar qu� productos mostrar en la tienda
        itemNameText.text = "Objeto a vender"; // Aqu� debes poner el nombre del objeto que el mercader vende
        itemPriceText.text = "Precio: 100 Bolsas"; // Ejemplo de texto para el precio en bolsas de monedas

        // Configura la funcionalidad del bot�n de compra
        buyButton.GetComponent<Button>().onClick.AddListener(BuyItem); // Agrega un listener al bot�n de compra
    }

    public void CloseShop()
    {
        shopInterface.SetActive(false);
        isShopOpen = false;

        // Limpia cualquier listener que pueda haber en el bot�n de compra
        buyButton.GetComponent<Button>().onClick.RemoveAllListeners();
    }

    void BuyItem()
    {
        // Aqu� implementa la l�gica para comprar el objeto, verificar monedas, etc.
        Debug.Log("Comprando el objeto...");

        // Por ejemplo, puedes restar las monedas del jugador si tiene suficientes
        // Puedes acceder al inventario del jugador a trav�s del script de PlayerMovement si es necesario
    }
}