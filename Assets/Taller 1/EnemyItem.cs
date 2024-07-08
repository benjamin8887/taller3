using UnityEngine;

public class EnemyItem : MonoBehaviour
{
    public GameObject itemDrop; // Objeto que el enemigo dejará al morir

    private void OnDestroy()
    {
        // Instanciar el objeto que se va a dejar caer
        GameObject go = Instantiate(itemDrop, transform.position, Quaternion.identity);
        go.name = itemDrop.name;

    }
}
