using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerrarJuegoConBoton : MonoBehaviour
{
    public void CerrarJuego()
    {
        // Sale de la aplicación
        Application.Quit();

        // Esta línea solo funcionará en el editor de Unity para detener la reproducción
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
