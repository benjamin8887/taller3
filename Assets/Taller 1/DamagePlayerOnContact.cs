using UnityEngine;

public class DamagePlayerOnContact : MonoBehaviour
{
    public int damageAmount = 100; // Cantidad de daño que se aplicará al jugador

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                // Aplicar el daño al jugador
                player.TakeDamage(damageAmount);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            if (player != null)
            {
                // Aplicar el daño al jugador
                player.TakeDamage(damageAmount);
            }
        }
    }
}
