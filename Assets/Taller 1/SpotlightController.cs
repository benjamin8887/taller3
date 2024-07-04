using UnityEngine;

public class SpotlightController : MonoBehaviour
{
    public float minIntensity = 1f;
    public float maxIntensity = 5f;
    public float frequency = 1f; // Frecuencia de parpadeo en segundos

    private Light spotlight;
    private float baseIntensity;
    private float timer;

    void Start()
    {
        spotlight = GetComponent<Light>();
        baseIntensity = spotlight.intensity;
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Cambia la intensidad entre minIntensity y maxIntensity
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.PingPong(timer * frequency, 1f));
        spotlight.intensity = baseIntensity * intensity;
    }
}
