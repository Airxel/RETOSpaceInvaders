using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundsManager : MonoBehaviour
{
    public AudioSource soundsSource, musicSource, bigInvaderSoundSource;

    public AudioClip playerShootingSound, playerDeadSound, invaderShootingSound, invaderDeadSound, specialExplosionSound;

    public Slider soundsSlider, musicSlider;

    //Singleton
    public static SoundsManager instance;

    private void Awake()
    {
        if (SoundsManager.instance == null)
        {
            SoundsManager.instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    /// <summary>
    /// Función que mueve los sliders al mismo valor inicial de los volúmenes
    /// </summary>
    private void Start()
    {
        //Sliders y volúmenes
        soundsSlider.value = soundsSource.volume;
        musicSlider.value = musicSource.volume;
    }

    /// <summary>
    /// Función que se llama desde el resto de scripts, para hacer sonar un sonido específico, según la situación
    /// </summary>
    /// <param name="sound"></param>
    public void PlaySound(AudioClip sound)
    {
        soundsSource.PlayOneShot(sound);
    }

    /// <summary>
    /// Función que controla el volúmen al cambiar los sliders una vez jugando
    /// </summary>
    public void SoundVolume()
    {
        soundsSource.volume = soundsSlider.value;
        bigInvaderSoundSource.volume = soundsSlider.value;
        musicSource.volume = musicSlider.value;
    }
}
