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
    /// Funci�n que mueve los sliders al mismo valor inicial de los vol�menes
    /// </summary>
    private void Start()
    {
        //Sliders y vol�menes
        soundsSlider.value = soundsSource.volume;
        musicSlider.value = musicSource.volume;
    }

    /// <summary>
    /// Funci�n que se llama desde el resto de scripts, para hacer sonar un sonido espec�fico, seg�n la situaci�n
    /// </summary>
    /// <param name="sound"></param>
    public void PlaySound(AudioClip sound)
    {
        soundsSource.PlayOneShot(sound);
    }

    /// <summary>
    /// Funci�n que controla el vol�men al cambiar los sliders una vez jugando
    /// </summary>
    public void SoundVolume()
    {
        soundsSource.volume = soundsSlider.value;
        bigInvaderSoundSource.volume = soundsSlider.value;
        musicSource.volume = musicSlider.value;
    }
}
