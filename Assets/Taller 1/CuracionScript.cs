using UnityEngine;

public class CuracionScript : MonoBehaviour
{
    public int curacionAmount = 25; // Cantidad de salud a curar

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.HealPlayer(curacionAmount); // Llama al m�todo HealPlayer del PlayerMovement
                Destroy(gameObject); // Destruye este objeto de curaci�n
            }
        }
    }
}
