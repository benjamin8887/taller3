using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float maxDistance = 10f; // Distancia máxima antes de destruir la bala si no colisiona
    public int damage = 15; // Daño que inflige la bala

    private float distanceTraveled = 0f; // Distancia recorrida por la bala
    private bool hasHit = false; // Flag para verificar si la bala ha colisionado
    Vector2 initialPos;

    private void Start()
    {
        initialPos = this.transform.position;
    }

    void Update()
    {
        // Mover la bala en la dirección del eje x
        // transform.Translate(Vector2.right * speed * Time.deltaTime);

        // Actualizar la distancia recorrida por la bala
        // distanceTraveled += speed * Time.deltaTime;

        // Destruir la bala si ha alcanzado la distancia máxima sin colisionar
        if (!hasHit && Vector2.Distance(initialPos,this.transform.position) >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

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
            }

            // Marcar que la bala ha impactado con un enemigo
            hasHit = true;

            // Destruir la bala al impactar con el enemigo
            Destroy(gameObject);
        }
    }
}
