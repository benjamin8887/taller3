using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage = 10; // Daño que causa al jugador
    public int maxHealth = 100; // Salud máxima del enemigo
    private int currentHealth; // Salud actual del enemigo
    public int scoreValue = 10; // Puntos que otorga al ser eliminado
    private ScoreManager scoreManager; // Referencia al ScoreManager
    public float speed = 5f; // Velocidad de movimiento del enemigo
    private Transform target; // Referencia al jugador
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private bool grounded = false; // Indica si el enemigo está en el suelo

    // Variables para el raycast
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer;

    void Start()
    {
        currentHealth = maxHealth; // Al comienzo, la salud actual es igual a la salud máxima
        scoreManager = FindObjectOfType<ScoreManager>(); // Buscar el ScoreManager en la escena
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager no encontrado en la escena.");
        }

        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtener el componente SpriteRenderer
        rb = GetComponent<Rigidbody2D>(); // Obtener el Rigidbody2D del enemigo

        // Buscar al jugador por su tag "Player"
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if (target == null)
        {
            Debug.LogError("No se encontró al jugador con la etiqueta 'Player'.");
        }
    }

    void Update()
    {
        // Verificar si el enemigo está en el suelo
        grounded = IsGrounded();

        // Si no estamos en el suelo, detener el movimiento
        if (!grounded)
        {
            rb.velocity = Vector2.zero;
            return; // Salir del método Update si no estamos en el suelo
        }

        // Si estamos en el suelo y hay un objetivo válido (jugador), seguir al jugador
        if (target != null)
        {
            float moveDirection = (target.position - transform.position).normalized.x;

            // Movimiento horizontal
            rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);

            // Voltear el sprite según la dirección del movimiento
            if (moveDirection < 0) // Movimiento hacia la izquierda
            {
                spriteRenderer.flipX = true; // Invertir el sprite horizontalmente
            }
            else if (moveDirection > 0) // Movimiento hacia la derecha
            {
                spriteRenderer.flipX = false; // Restaurar la orientación normal del sprite
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si ha chocado con el jugador
        if (other.CompareTag("Player"))
        {
            // Obtener la posición relativa entre el enemigo y el jugador
            Vector2 relativePosition = other.transform.position - transform.position;

            // Si la posición relativa en Y es positiva (el jugador está encima del enemigo)
            if (relativePosition.y > 0)
            {
                Die(); // Llamar al método Die para que el enemigo muera al ser pisado desde arriba
            }
            else
            {
                // Obtener el componente de salud del jugador (esto depende de cómo esté implementado en tu juego)
                PlayerMovement player = other.GetComponent<PlayerMovement>();

                // Si el componente de salud existe, hacer daño al jugador
                if (player != null)
                {
                    player.TakeDamage(damage);
                }
            }
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

    bool IsGrounded()
    {
        // Raycast hacia abajo para verificar si el enemigo está en el suelo
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, Color.green);

        return hit.collider != null;
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
                Die(); // Llamar al método Die para que el enemigo muera al ser pisado desde arriba
            }
            else
            {
                PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
                if (player != null)
                {
                    player.TakeDamage(damage); // Aplicar daño al jugador si entra en contacto con el enemigo
                }
            }
        }
    }
}
