using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsButton : MonoBehaviour
{
    [SerializeField] private Slider _volumeSlider;

    [SerializeField] private AudioSource _audioSource;

    public void Start()
    {
    }

    public void ChangeVolume()
    {
        AudioListener.volume = _volumeSlider.value;
        Debug.Log("volume :" + _volumeSlider.value);
    }

    public void FullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;

        Debug.Log(Screen.fullScreenMode);
    }

    public void Audio()
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        } else
        {
            _audioSource.Play();
        }
    }
}

