using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damageAmount = 10; // Da�o que har� el proyectil al jugador

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.TakeDamage(damageAmount);
            }
            Destroy(gameObject); // Destruir el proyectil despu�s de impactar con el jugador
        }
    }
}
