using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    public int damage = 25;
    public int maxHealth = 100; // Salud m�xima del enemigo
    private int currentHealth; // Salud actual del enemigo
    public int scoreValue = 10; // Puntos que otorga al ser eliminado
    private ScoreManager scoreManager; // Referencia al ScoreManager

    void Start()
    {
        currentHealth = maxHealth; // Al comienzo, la salud actual es igual a la salud m�xima
        scoreManager = FindObjectOfType<ScoreManager>(); // Buscar el ScoreManager en la escena
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager no encontrado en la escena.");
        }
    }

    // M�todo para recibir da�o
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Restar el da�o a la salud actual

        if (currentHealth <= 0)
        {
            Die(); // Llamar a la funci�n de muerte si la salud llega a cero o menos
        }
    }

    // M�todo para la muerte del enemigo
    void Die()
    {
        // Aqu� puedes agregar la l�gica para la muerte del enemigo, como reproducir una animaci�n, 
        // reproducir un efecto de sonido, otorgar puntos al jugador, etc.
        Debug.Log("El enemigo ha muerto.");

        if (scoreManager != null)
        {
            scoreManager.AddPoints(scoreValue); // A�adir puntos al ScoreManager al morir
        }

        Destroy(gameObject); // Destruir el GameObject del enemigo
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Obtener la posici�n relativa entre el enemigo y el jugador
            Vector2 relativePosition = collision.transform.position - transform.position;

            // Si la posici�n relativa en Y es positiva (el jugador est� encima del enemigo)
            if (relativePosition.y > 0)
            {
                PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
                if (player != null)
                {
                    player.TakeDamage(damage); // Aplicar da�o al jugador si entra en contacto con el enemigo
                }
            }
            else
            {
                // Si el jugador colisiona desde los lados o abajo del enemigo, el jugador recibe da�o
                PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
                if (player != null)
                {
                    player.TakeDamage(damage);
                }
            }
        }
    }
}
