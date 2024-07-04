using UnityEngine;

public class LanternController : MonoBehaviour
{
    private Light spotlight;

    void Start()
    {
        spotlight = GetComponentInChildren<Light>();
        if (spotlight != null)
        {
            spotlight.enabled = false; // Empieza apagada
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) // Presiona 'L' para alternar la linterna
        {
            spotlight.enabled = !spotlight.enabled;
        }
    }
}
