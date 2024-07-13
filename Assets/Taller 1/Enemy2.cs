using UnityEngine;

public class CombinedEnemy : MonoBehaviour
{
    public int damage = 10; // Daño que causa al jugador
    public int maxHealth = 100; // Salud máxima del enemigo
    private int currentHealth; // Salud actual del enemigo
    public int scoreValue = 10; // Puntos que otorga al ser eliminado
    private ScoreManager scoreManager; // Referencia al ScoreManager
    public float speed = 5f; // Velocidad de movimiento del enemigo
    public float visionRange = 5f; // Rango de visión para detectar al jugador
    private Transform player; // Referencia al jugador
    [SerializeField] Collider2D myCollider;

    [SerializeField] bool canDieFromJump = true;

    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer del enemigo

    void Start()
    {
        currentHealth = maxHealth; // Al comienzo, la salud actual es igual a la salud máxima
        scoreManager = FindObjectOfType<ScoreManager>(); // Buscar el ScoreManager en la escena
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager no encontrado en la escena.");
        }

        // Encontrar al jugador al inicio
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Physics2D.IgnoreCollision(myCollider, player.GetComponent<Collider2D>());

        // Obtener el componente SpriteRenderer del enemigo
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Si el jugador existe y está dentro del rango de visión, mover hacia él en el eje X
        if (player != null && IsPlayerInVision())
        {
            Vector2 targetPosition = new Vector2(player.position.x, transform.position.y);

            // Raycast para detectar obstáculos frente al enemigo
            RaycastHit2D hit = Physics2D.Raycast(transform.position, targetPosition - (Vector2)transform.position, Vector2.Distance(transform.position, targetPosition));
            if (hit.collider != null && hit.collider.CompareTag("Wall"))
            {
                // Si hay una pared, detener el movimiento del enemigo
                Debug.DrawRay(transform.position, targetPosition - (Vector2)transform.position, Color.red);
            }
            else
            {
                // Si no hay obstáculos, mover al enemigo
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                Debug.DrawRay(transform.position, targetPosition - (Vector2)transform.position, Color.green);
            }

            // Voltear el sprite según la dirección del movimiento
            FlipSprite(targetPosition.x - transform.position.x);
        }
    }

    bool IsPlayerInVision()
    {
        // Calcula la distancia al jugador
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        return distanceToPlayer <= visionRange;
    }

    void FlipSprite(float direction)
    {
        if (direction < 0) // Movimiento hacia la izquierda
        {
            spriteRenderer.flipX = true; // Invertir el sprite horizontalmente
        }
        else if (direction > 0) // Movimiento hacia la derecha
        {
            spriteRenderer.flipX = false; // Restaurar la orientación normal del sprite
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
            if (relativePosition.y > 0 && canDieFromJump)
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
}
