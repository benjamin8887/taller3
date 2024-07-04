using UnityEngine;

public class PuertaScript : MonoBehaviour
{
    public GameObject puertaCerrada; // GameObject de la puerta cerrada
    public GameObject puertaAbierta; // GameObject de la puerta abierta
    public GameObject llave; // Referencia al GameObject de la llave

    private bool puertaEstaCerrada = true; // Estado inicial de la puerta (cerrada)

    void Start()
    {
        // Al iniciar, asegurarse de que solo la puerta cerrada esté activa
        puertaCerrada.SetActive(true);
        puertaAbierta.SetActive(false);
    }

    public void AbrirPuerta()
    {
        if (puertaEstaCerrada)
        {
            puertaCerrada.SetActive(false); // Desactivar la puerta cerrada
            puertaAbierta.SetActive(true); // Activar la puerta abierta
            puertaEstaCerrada = false; // Actualizar estado de la puerta
        }
    }

    public void DesaparecerLlave()
    {
        if (llave != null)
        {
            Destroy(llave); // Destruir la llave al recogerla
        }
    }

    public bool EstaBloqueandoPaso()
    {
        return puertaEstaCerrada; // Devuelve true si la puerta está cerrada
    }
}
