using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider; // Referencia al componente Slider para controlar la barra de vida

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth; // Establece el valor máximo de la barra de vida
        slider.value = maxHealth; // Establece el valor actual de la barra de vida al máximo
    }

    public void SetHealth(int health)
    {
        slider.value = health; // Establece el valor actual de la barra de vida
    }
}

