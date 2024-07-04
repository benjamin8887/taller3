using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource musicSource;

    private void Start()
    {
        // Verificar si ya hay un AudioManager en la escena
        GameObject[] objs = GameObject.FindGameObjectsWithTag("AudioManager");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }

        // Asignar el valor inicial del slider al volumen actual de la música
        volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        musicSource.volume = volumeSlider.value;
    }

    private void Update()
    {
        // Actualizar el volumen de la música con el valor del slider
        musicSource.volume = volumeSlider.value;
        // Guardar el valor del volumen en PlayerPrefs para que persista entre escenas
        PlayerPrefs.SetFloat("MusicVolume", volumeSlider.value);
    }
}
