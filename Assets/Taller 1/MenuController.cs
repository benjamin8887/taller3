using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Nombre de la escena del menú principal
    public string mainMenuSceneName = "Main Menu";

    // Método para cargar la escena del menú principal
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
