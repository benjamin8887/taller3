using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResumeButton : MonoBehaviour
{
    // Método para reanudar el juego
    public void ResumeGame()
    {
        // Desactivar el menú de pausa
        Time.timeScale = 1f; // Reanudar el tiempo en el juego
        gameObject.SetActive(false); // Desactivar este menú de pausa

        // Si necesitas hacer alguna otra acción al reanudar el juego, puedes agregarla aquí
    }
}
