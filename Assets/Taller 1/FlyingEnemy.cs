using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento hacia el jugador
    public float attackDistance = 5f; // Distancia a la que el enemigo atacar� al jugador
    public float visionRange = 10f; // Rango de visi�n del enemigo
    public float visionAngle = 90f; // �ngulo de visi�n del enemigo (ahora es 90 grados para visi�n en todos los lados)
    public int damage = 25; // Da�o que hace el enemigo al jugador

    private Transform player;
    private bool isAttacking = false;
    private float attackCooldown = 2f; // Tiempo de enfriamiento entre ataques
    private float attackTimer = 0f; // Contador para controlar el tiempo entre ataques

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null)
            return;

        // Calcula la distancia al jugador
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Calcula la direcci�n al jugador
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        // Verifica si el jugador est� dentro del rango de visi�n
        if (distanceToPlayer <= visionRange && Vector2.Angle(Vector2.right, directionToPlayer) <= visionAngle / 2)
        {
            // Si el jugador tambi�n est� dentro del rango de ataque
            if (distanceToPlayer <= attackDistance)
            {
                // Atacar al jugador si no est� atacando actualmente y el temporizador ha pasado
                if (!isAttacking && attackTimer <= 0)
                {
                    AttackPlayer();
                    attackTimer = attackCooldown;
                }
            }
            else
            {
                // Mover hacia el jugador si no est� atacando
                if (!isAttacking)
                {
                    MoveTowardsPlayer(directionToPlayer);
                }
            }
        }
        else
        {
            // Si el jugador est� fuera del rango de visi�n, no atacar
            isAttacking = false;
        }

        // Actualiza el temporizador de ataque
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    void MoveTowardsPlayer(Vector2 directionToPlayer)
    {
        // Mueve el enemigo hacia el jugador
        transform.Translate(directionToPlayer * speed * Time.deltaTime);
    }

    void AttackPlayer()
    {
        // Implementa aqu� tu l�gica de ataque al jugador
        // Por ejemplo, podr�as reducir la salud del jugador o activar alg�n efecto de da�o

        // Aqu� se asume que el jugador tiene un script de salud (Health) que podemos llamar
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.TakeDamage(damage);
        }

        // Marca como atacando para evitar ataques repetidos
        isAttacking = true;

        // Despu�s de un tiempo, deja de atacar (por ejemplo, espera unos segundos)
        Invoke("StopAttacking", 2f);
    }

    void StopAttacking()
    {
        isAttacking = false;
    }
}
