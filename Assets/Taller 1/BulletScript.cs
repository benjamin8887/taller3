using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float maxDistance = 10f; // Distancia máxima antes de destruir la bala si no colisiona
    public int damage = 15; // Daño que inflige la bala
    public float knockbackForce = 100f; // Fuerza de empuje hacia atrás

    private bool hasHit = false; // Flag para verificar si la bala ha colisionado

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si la bala ha colisionado con un enemigo
        if (other.CompareTag("Enemy"))
        {
            // Obtener el componente EnemyHealth del enemigo
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                // Aplicar daño al enemigo
                enemyHealth.TakeDamage(damage);

                // Obtener la dirección desde la bala hacia el enemigo
                Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;

                // Aplicar el knockback al enemigo
                enemyHealth.Knockback(knockbackDirection, knockbackForce);
            }

            // Marcar que la bala ha impactado con un enemigo
            hasHit = true;

            // Destruir la bala al impactar con el enemigo
            Destroy(gameObject);
        }
    }
}
