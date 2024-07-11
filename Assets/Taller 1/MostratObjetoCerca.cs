using UnityEngine;

public class MostrarObjetoCerca : MonoBehaviour
{
    public Transform jugador;  // Referencia al transform del jugador
    public Transform objeto;   // Referencia al transform del objeto que queremos mostrar

    public float distanciaMostrar = 5f;  // Distancia a la cual se mostrará el objeto

    private Renderer objetoRenderer;  // Referencia al componente Renderer del objeto

    void Start()
    {
        // Obtener el componente Renderer del objeto
        objetoRenderer = objeto.GetComponent<Renderer>();
        if (objetoRenderer == null)
        {
            Debug.LogError("El objeto no tiene un componente Renderer.");
            enabled = false;  // Desactivar el script si no se encuentra el componente Renderer
        }

        // Inicialmente, desactivar el objeto
        objetoRenderer.enabled = false;
    }

    void Update()
    {
        // Calcular la distancia entre el jugador y el objeto
        float distancia = Vector2.Distance(jugador.position, objeto.position);

        // Verificar si el jugador está lo suficientemente cerca para mostrar u ocultar el objeto
        if (distancia <= distanciaMostrar)
        {
            objetoRenderer.enabled = true;  // Mostrar el objeto
        }
        else
        {
            objetoRenderer.enabled = false;  // Ocultar el objeto
        }
    }
}
