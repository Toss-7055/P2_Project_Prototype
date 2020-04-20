using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; // Does so you can work with the audio in your project. See https://www.youtube.com/watch?v=YOaYQrN1oYQ for more

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer; // Makes the AudioMixer able to be worked with

    public void SetVolume(float volume) // Function that allows you to set the volume of the audio in the game. This only affects the master volume. 
    {
        audioMixer.SetFloat("Volume", volume); 
    }
}
