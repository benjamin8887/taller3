using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float moveSpeed = 5f;  // Velocidad de movimiento del enemigo
    public int damageAmount = 1;  // Da�o que hace el enemigo al jugador por golpe
    public int maxHealth = 100;    // Vida m�xima del enemigo

    private int currentHealth;    // Vida actual del enemigo

    private Transform player;     // Referencia al jugador
    private Rigidbody2D rb;       // Rigidbody del enemigo
    private SpriteRenderer spriteRenderer; // Componente SpriteRenderer del enemigo

    private ScoreManager scoreManager; // Referencia al ScoreManager
    public int scoreValue = 10; // Puntos que otorga al ser eliminado
    public float visionRange = 5f; // Rango de visi�n para detectar al jugador
    [SerializeField] bool dieFromJump = true;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtener el componente SpriteRenderer
        scoreManager = FindObjectOfType<ScoreManager>(); // Buscar el ScoreManager en la escena

        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager no encontrado en la escena.");
        }
    }

    void Update()
    {
        // Mover hacia el jugador si est� dentro del rango de visi�n
        if (player != null && IsPlayerInVision())
        {
            Vector2 moveDirection = (player.position - transform.position).normalized;
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

            // Voltear el sprite seg�n la direcci�n del movimiento
            FlipSprite(moveDirection.x);
        }
        else
        {
            rb.velocity = Vector2.zero; // Detener el movimiento si el jugador no est� dentro del rango
        }
    }

    bool IsPlayerInVision()
    {
        // Calcula la distancia al jugador
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        return distanceToPlayer <= visionRange;
    }

    void FlipSprite(float moveDirectionX)
    {
        if (moveDirectionX > 0) // Movimiento hacia la derecha
        {
            spriteRenderer.flipX = false; // Restaurar la orientaci�n normal del sprite
        }
        else if (moveDirectionX < 0) // Movimiento hacia la izquierda
        {
            spriteRenderer.flipX = true; // Invertir el sprite horizontalmente
        }
        // No es necesario hacer nada si moveDirectionX == 0 porque ya mantendr�a la orientaci�n actual del sprite
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si ha chocado con el jugador
        if (other.CompareTag("Player"))
        {
            // Obtener la posici�n relativa entre el enemigo y el jugador
            Vector2 relativePosition = other.transform.position - transform.position;

            // Si la posici�n relativa en Y es positiva (el jugador est� encima del enemigo)
            if (relativePosition.y > 0 && dieFromJump)
            {
                Die(); // Llamar al m�todo Die para que el enemigo muera al ser pisado desde arriba
            }
            else
            {
                // Obtener el componente de salud del jugador (esto depende de c�mo est� implementado en tu juego)
                PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

                // Si el componente de salud existe, hacer da�o al jugador
                if (playerMovement != null)
                {
                    playerMovement.TakeDamage(damageAmount);
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

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
}
