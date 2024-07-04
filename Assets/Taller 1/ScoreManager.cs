using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText; // Referencia al componente de texto para mostrar el puntaje
    private int score; // Puntaje actual del jugador

    void Start()
    {
        score = 0; // Inicializar el puntaje a cero al inicio del juego
        UpdateScoreText(); // Actualizar el texto inicialmente
    }

    public void AddPoints(int pointsToAdd)
    {
        score += pointsToAdd; // Sumar puntos al puntaje actual
        UpdateScoreText(); // Actualizar el texto del puntaje después de sumar puntos
    }

    void UpdateScoreText()
    {
        // Actualizar el componente de texto con el puntaje actual
        scoreText.text = "Score: " + score.ToString();
    }
}
