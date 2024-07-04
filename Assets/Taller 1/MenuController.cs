using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Nombre de la escena del men� principal
    public string mainMenuSceneName = "Main Menu";

    // M�todo para cargar la escena del men� principal
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
