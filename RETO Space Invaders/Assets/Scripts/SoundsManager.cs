using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundsManager : MonoBehaviour
{
    public AudioSource soundsSource, musicSource, bigInvaderSoundSource;

    public AudioClip playerShootingSound, playerDeadSound, invaderShootingSound, invaderDeadSound;

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

    private void Start()
    {
        //Sliders y volúmenes
        soundsSlider.value = soundsSource.volume;
        musicSlider.value = musicSource.volume;
    }

    public void PlaySound(AudioClip sound)
    {
        soundsSource.PlayOneShot(sound);
    }

    public void SoundVolume()
    {
        soundsSource.volume = soundsSlider.value;
        bigInvaderSoundSource.volume = soundsSlider.value;
        musicSource.volume = musicSlider.value;
    }
}
