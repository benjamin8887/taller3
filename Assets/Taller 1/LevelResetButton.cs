using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelResetButton : MonoBehaviour
{
    public string levelToLoad = "NombreDeTuEscena"; // Nombre de la escena a cargar

    public void ResetLevel()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
