using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Variable para almacenar la posición del checkpoint
    private Vector3 checkpointPosition;

    // Método para guardar la posición del checkpoint
    public void SaveCheckpoint(Vector3 position)
    {
        checkpointPosition = position;
    }

    // Método para obtener la posición del checkpoint
    public Vector3 GetCheckpointPosition()
    {
        return checkpointPosition;
    }

    // Método para respawnear el jugador en el checkpoint
    public void RespawnPlayer(GameObject player)
    {
        player.transform.position = checkpointPosition;
        // Aquí puedes agregar cualquier otra lógica de respawn que necesites (restablecer vida, etc.)
    }

    // Opcional: Método para reiniciar el checkpoint
    public void ResetCheckpoint()
    {
        checkpointPosition = Vector3.zero; // Reinicias la posición del checkpoint si es necesario
    }

    // Opcional: Método para inicializar el checkpoint (por ejemplo, en el Start)
    private void Start()
    {
        ResetCheckpoint();
    }
}
