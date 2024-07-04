using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Variable para almacenar la posici�n del checkpoint
    private Vector3 checkpointPosition;

    // M�todo para guardar la posici�n del checkpoint
    public void SaveCheckpoint(Vector3 position)
    {
        checkpointPosition = position;
    }

    // M�todo para obtener la posici�n del checkpoint
    public Vector3 GetCheckpointPosition()
    {
        return checkpointPosition;
    }

    // M�todo para respawnear el jugador en el checkpoint
    public void RespawnPlayer(GameObject player)
    {
        player.transform.position = checkpointPosition;
        // Aqu� puedes agregar cualquier otra l�gica de respawn que necesites (restablecer vida, etc.)
    }

    // Opcional: M�todo para reiniciar el checkpoint
    public void ResetCheckpoint()
    {
        checkpointPosition = Vector3.zero; // Reinicias la posici�n del checkpoint si es necesario
    }

    // Opcional: M�todo para inicializar el checkpoint (por ejemplo, en el Start)
    private void Start()
    {
        ResetCheckpoint();
    }
}
