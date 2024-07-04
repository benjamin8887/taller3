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
                playerMovement.HealPlayer(curacionAmount); // Llama al método HealPlayer del PlayerMovement
                Destroy(gameObject); // Destruye este objeto de curación
            }
        }
    }
}
