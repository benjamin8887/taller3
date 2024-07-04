using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Checkpoint currentCheckpoint; // Referencia al script del checkpoint actual

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            currentCheckpoint = other.GetComponent<Checkpoint>();
            currentCheckpoint.SaveCheckpoint(transform.position);
        }
    }

    // M�todo para respawnear el jugador usando el checkpoint guardado
    private void Respawn()
    {
        if (currentCheckpoint != null)
        {
            currentCheckpoint.RespawnPlayer(gameObject);
            // Aqu� puedes agregar cualquier otra l�gica de respawn que necesites (por ejemplo, restablecer vida)
        }
        else
        {
            Debug.LogWarning("No se ha guardado ning�n checkpoint.");
            // Manejo de error o situaci�n donde no hay checkpoint guardado
        }
    }
}
