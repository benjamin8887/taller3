using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerrarJuegoConBoton : MonoBehaviour
{
    public void CerrarJuego()
    {
        // Sale de la aplicaci�n
        Application.Quit();

        // Esta l�nea solo funcionar� en el editor de Unity para detener la reproducci�n
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
