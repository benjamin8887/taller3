using UnityEngine;

public class EnemyItem : MonoBehaviour
{
    public GameObject itemDrop; // Objeto que el enemigo dejará al morir

    private void Die()
    {
        // Instanciar el objeto que se va a dejar caer
        Instantiate(itemDrop, transform.position, Quaternion.identity);

        // Destruir al enemigo
        Destroy(gameObject);
    }
}
