using UnityEngine;

public class RainDropCollision : MonoBehaviour
{
    public GameObject splashEffectPrefab; // Prefab para el efecto de la gota al tocar el suelo

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Ground")) // Asegúrate de que el suelo tenga la etiqueta "Ground"
        {
            // Instancia el efecto de salpicadura en la posición de la colisión
            foreach (ParticleCollisionEvent collisionEvent in GetCollisionEvents(other))
            {
                Vector3 collisionPosition = collisionEvent.intersection;
                Instantiate(splashEffectPrefab, collisionPosition, Quaternion.identity);
            }

            // Opcional: Puedes hacer que la gota desaparezca o se desactive aquí
            Destroy(gameObject); // Esto destruirá el sistema de partículas entero si lo deseas
        }
    }

    private ParticleCollisionEvent[] GetCollisionEvents(GameObject other)
    {
        ParticleCollisionEvent[] collisionEvents = new ParticleCollisionEvent[16];
        int numCollisionEvents = GetComponent<ParticleSystem>().GetCollisionEvents(other, collisionEvents);
        return collisionEvents;
    }
}
