using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResumeButton : MonoBehaviour
{
    // M�todo para reanudar el juego
    public void ResumeGame()
    {
        // Desactivar el men� de pausa
        Time.timeScale = 1f; // Reanudar el tiempo en el juego
        gameObject.SetActive(false); // Desactivar este men� de pausa

        // Si necesitas hacer alguna otra acci�n al reanudar el juego, puedes agregarla aqu�
    }
}
