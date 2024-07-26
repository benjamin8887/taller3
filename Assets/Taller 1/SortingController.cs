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
        // Ordenar bas�ndose en la posici�n Y del jugador
        foreach (Renderer renderer in renderers)
        {
            if (renderer.isVisible)
            {
                // Comparar la posici�n Y del jugador con la posici�n Y del objeto
                if (playerTransform.position.y > renderer.transform.position.y)
                {
                    renderer.sortingOrder = 1; // Poner detr�s del jugador
                }
                else
                {
                    renderer.sortingOrder = -1; // Poner delante del jugador
                }
            }
        }
    }
}
