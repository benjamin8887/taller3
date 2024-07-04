using UnityEngine;

public class LlaveScript : MonoBehaviour
{
    public PuertaScript puerta; // Referencia al script de la puerta para abrir la puerta

    public AudioClip sonidoLlave; // Sonido que se reproducirá al recoger la llave
    private AudioSource audioSource; // Componente AudioSource para reproducir sonidos

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Obtener componente AudioSource del objeto actual
        audioSource.clip = sonidoLlave; // Asignar el sonido de la llave al AudioSource
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Cuando el jugador toca la llave, abrir la puerta, reproducir el sonido de la llave y hacer desaparecer la llave
            puerta.AbrirPuerta();
            audioSource.Play(); // Reproducir el sonido de la llave
            puerta.DesaparecerLlave(); // Desaparecer la llave después de ser recogida
        }
    }
}
