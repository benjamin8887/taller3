using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private Rigidbody2D rb; // Rigidbody2D del enemigo

    // Variable para controlar la inmunidad al pisar o tocar
    private bool immuneToTouch = false;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>(); // Obtener el Rigidbody2D del enemigo
    }

    public void TakeDamage(int damage)
    {
        // Verificar si el enemigo es inmune al toque
        if (immuneToTouch)
        {
            Debug.Log("Enemy is immune to touch damage.");
            return; // Salir del m�todo sin recibir da�o
        }

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // M�todo para activar la inmunidad al toque
    public void ActivateTouchImmunity()
    {
        immuneToTouch = true;
        Debug.Log("Enemy is now immune to touch damage.");
    }

    // M�todo para desactivar la inmunidad al toque
    public void DeactivateTouchImmunity()
    {
        immuneToTouch = false;
        Debug.Log("Enemy is no longer immune to touch damage.");
    }

    void Die()
    {
        Destroy(gameObject);
    }

    // M�todo para empujar al enemigo hacia atr�s
    public void Knockback(Vector2 direction, float force)
    {
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }
}
