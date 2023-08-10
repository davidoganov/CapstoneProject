using UnityEngine.Audio;
using System;
using UnityEngine;

// this file manages the audio by containing various sounds within the list.
// credit: Brackeys
public class AudioManager : MonoBehaviour
{
    // establish an instance 
    public static AudioManager instance;

    // init the list of sounds
    public Sound[] sounds;

    void Awake()
    {
        // check if the instance is being used already
        if (instance == null)
        {
            // init it and dont destroy it when loading new scenes
            instance = this; 
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null)
        {
            // if it is, destroy the object
            Destroy(gameObject);
            return;
        }

        // loop thru each of the sounds and adjust each to a baseline set in sound.cs
        foreach (Sound sfx in sounds)
        {
            sfx.source = gameObject.AddComponent<AudioSource>();
            sfx.source.clip = sfx.clip;
            sfx.source.loop = sfx.loop;
        }

        // play the theme of the LingoMon game
        Play("Theme");
    }

    // plays the selected sound based off provided string input name of sound
    public void Play(string sound)
    {
        // find the sound
        Sound sfx = Array.Find(sounds, item => item.name == sound);

        // check if the sound is null
        if (sfx == null)
        {
            // display error warning
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        // adjust the volume and pitch
        sfx.source.volume = sfx.volume; // * (1f + UnityEngine.Random.Range(-sfx.volumeVariance / 2f, sfx.volumeVariance / 2f));
        sfx.source.pitch = sfx.pitch; // * (1f + UnityEngine.Random.Range(-sfx.pitchVariance / 2f, sfx.pitchVariance / 2f));

        // play the sound
        sfx.source.Play();
    }

    // stops the sound being played, given the name
    public void Stop(string sound)
    {
        // find the sound 
        Sound sfx = Array.Find(sounds, item => item.name == sound);

        // if its not null, stop playing it
        if (sfx != null)
        {
            sfx.source.Stop();
        }
    }
}
