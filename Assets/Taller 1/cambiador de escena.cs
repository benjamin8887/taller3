using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioDeEscena : MonoBehaviour
{
    // Nombre de la escena a la que quieres cambiar
    public string nombreDeLaEscena;

    // Método para cambiar a otra escena
    public void CambiarEscena()
    {
        // Cargar la escena con el nombre proporcionado
        SceneManager.LoadScene(nombreDeLaEscena);
    }
}
