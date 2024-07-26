using UnityEngine;

public class SortingController : MonoBehaviour
{
    public Transform playerTransform;
    private Renderer[] renderers;

    void Start()
    {
        // Obtener todos los renderers de los objetos que necesitas ordenar
        renderers = FindObjectsOfType<Renderer>();
    }

    void Update()
    {
        // Ordenar basándose en la posición Y del jugador
        foreach (Renderer renderer in renderers)
        {
            if (renderer.isVisible)
            {
                // Comparar la posición Y del jugador con la posición Y del objeto
                if (playerTransform.position.y > renderer.transform.position.y)
                {
                    renderer.sortingOrder = 1; // Poner detrás del jugador
                }
                else
                {
                    renderer.sortingOrder = -1; // Poner delante del jugador
                }
            }
        }
    }
}
