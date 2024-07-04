using UnityEngine;

public class EnemyEffect : MonoBehaviour
{
    public float slowDownFactor = 0.5f; // Factor de desaceleración del jugador
    public float damagePerSecond = 5f; // Daño por segundo al jugador

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.ApplyEffect(slowDownFactor, damagePerSecond);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.RemoveEffect();
            }
        }
    }
}
