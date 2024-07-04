using UnityEngine;
using Cinemachine;

public class MoveCamera : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Transform targetPosition;

    void Start()
    {
        if (virtualCamera == null)
            virtualCamera = GetComponent<CinemachineVirtualCamera>();

        // Movemos la cámara a la posición del objetivo (en este caso, el jugador)
        virtualCamera.transform.position = targetPosition.position;
    }
}
