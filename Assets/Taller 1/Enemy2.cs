using UnityEngine;

public class CombinedEnemy : MonoBehaviour
{
    public int damage = 10; // Da�o que causa al jugador
    public int maxHealth = 100; // Salud m�xima del enemigo
    private int currentHealth; // Salud actual del enemigo
    public int scoreValue = 10; // Puntos que otorga al ser eliminado
    private ScoreManager scoreManager; // Referencia al ScoreManager
    public float speed = 5f; // Velocidad de movimiento del enemigo
    public float visionRange = 5f; // Rango de visi�n para detectar al jugador
    private Transform player; // Referencia al jugador
    [SerializeField] Collider2D myCollider;

    [SerializeField] bool canDieFromJump = true;

    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer del enemigo

    void Start()
    {
        currentHealth = maxHealth; // Al comienzo, la salud actual es igual a la salud m�xima
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
        // Si el jugador existe y est� dentro del rango de visi�n, mover hacia �l en el eje X
        if (player != null && IsPlayerInVision())
        {
            Vector2 targetPosition = new Vector2(player.position.x, transform.position.y);

            // Raycast para detectar obst�culos frente al enemigo
            RaycastHit2D hit = Physics2D.Raycast(transform.position, targetPosition - (Vector2)transform.position, Vector2.Distance(transform.position, targetPosition));
            if (hit.collider != null && hit.collider.CompareTag("Wall"))
            {
                // Si hay una pared, detener el movimiento del enemigo
                Debug.DrawRay(transform.position, targetPosition - (Vector2)transform.position, Color.red);
            }
            else
            {
                // Si no hay obst�culos, mover al enemigo
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                Debug.DrawRay(transform.position, targetPosition - (Vector2)transform.position, Color.green);
            }

            // Voltear el sprite seg�n la direcci�n del movimiento
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
            spriteRenderer.flipX = false; // Restaurar la orientaci�n normal del sprite
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si ha chocado con el jugador
        if (other.CompareTag("Player"))
        {
            // Obtener la posici�n relativa entre el enemigo y el jugador
            Vector2 relativePosition = other.transform.position - transform.position;

            // Si la posici�n relativa en Y es positiva (el jugador est� encima del enemigo)
            if (relativePosition.y > 0 && canDieFromJump)
            {
                Die(); // Llamar al m�todo Die para que el enemigo muera al ser pisado desde arriba
            }
            else
            {
                // Obtener el componente de salud del jugador (esto depende de c�mo est� implementado en tu juego)
                PlayerMovement player = other.GetComponent<PlayerMovement>();

                // Si el componente de salud existe, hacer da�o al jugador
                if (player != null)
                {
                    player.TakeDamage(damage);
                }
            }
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
}
