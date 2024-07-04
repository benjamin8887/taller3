using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    public int damage = 25;
    public int maxHealth = 100; // Salud máxima del enemigo
    private int currentHealth; // Salud actual del enemigo
    public int scoreValue = 10; // Puntos que otorga al ser eliminado
    private ScoreManager scoreManager; // Referencia al ScoreManager

    void Start()
    {
        currentHealth = maxHealth; // Al comienzo, la salud actual es igual a la salud máxima
        scoreManager = FindObjectOfType<ScoreManager>(); // Buscar el ScoreManager en la escena
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager no encontrado en la escena.");
        }
    }

    // Método para recibir daño
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Restar el daño a la salud actual

        if (currentHealth <= 0)
        {
            Die(); // Llamar a la función de muerte si la salud llega a cero o menos
        }
    }

    // Método para la muerte del enemigo
    void Die()
    {
        // Aquí puedes agregar la lógica para la muerte del enemigo, como reproducir una animación, 
        // reproducir un efecto de sonido, otorgar puntos al jugador, etc.
        Debug.Log("El enemigo ha muerto.");

        if (scoreManager != null)
        {
            scoreManager.AddPoints(scoreValue); // Añadir puntos al ScoreManager al morir
        }

        Destroy(gameObject); // Destruir el GameObject del enemigo
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Obtener la posición relativa entre el enemigo y el jugador
            Vector2 relativePosition = collision.transform.position - transform.position;

            // Si la posición relativa en Y es positiva (el jugador está encima del enemigo)
            if (relativePosition.y > 0)
            {
                PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
                if (player != null)
                {
                    player.TakeDamage(damage); // Aplicar daño al jugador si entra en contacto con el enemigo
                }
            }
            else
            {
                // Si el jugador colisiona desde los lados o abajo del enemigo, el jugador recibe daño
                PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
                if (player != null)
                {
                    player.TakeDamage(damage);
                }
            }
        }
    }
}
