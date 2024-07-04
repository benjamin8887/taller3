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

    // Método para respawnear el jugador usando el checkpoint guardado
    private void Respawn()
    {
        if (currentCheckpoint != null)
        {
            currentCheckpoint.RespawnPlayer(gameObject);
            // Aquí puedes agregar cualquier otra lógica de respawn que necesites (por ejemplo, restablecer vida)
        }
        else
        {
            Debug.LogWarning("No se ha guardado ningún checkpoint.");
            // Manejo de error o situación donde no hay checkpoint guardado
        }
    }
}
